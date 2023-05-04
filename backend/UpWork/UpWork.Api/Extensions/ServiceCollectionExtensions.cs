using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UpWork.Api.Requirements;
using UpWork.Api.Requirements.Handlers;
using UpWork.Common.Enums;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Database;
using UpWork.Infrastucture.Services;

namespace UpWork.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomAuth(this IServiceCollection services, IConfiguration config)
        {
            if (string.IsNullOrEmpty(config["JwtSettings:Issuer"])
                    || string.IsNullOrEmpty(config["JwtSettings:Audience"])
                    || string.IsNullOrEmpty(config["JwtSettings:Key"]))
                throw new SecurityTokenException("Settings are empty");

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {

                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = config["JwtSettings:Issuer"],
                        ValidAudience = config["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityData.AdminUserPolicy, policy => policy.RequireClaim(IdentityData.AdminUserClaimName, "true"));

                options.AddPolicy(IdentityData.CreateUserPolicy,
                    policy => policy
                    .RequireClaim(IdentityData.OrganizationIdClaimName)
                    .RequireClaim(IdentityData.PermissionsClaimName, PermissionType.CreateUser.ToString()));

                options.AddPolicy(IdentityData.MatchOrganizationIdQueryPolicy, policy => policy.Requirements.Add(new MatchOrganizationQueryRequirement()));
                options.AddPolicy(IdentityData.MatchOrganizationIdBodyPolicy, policy => policy.Requirements.Add(new MatchOrganizationBodyRequirement()));
            });
        }

        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("Default");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(conn);
            });
        }

        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddSingleton<IAuthorizationHandler, MatchOrganizationQueryHandler>();
            services.AddSingleton<IAuthorizationHandler, MatchOrganizationBodyHandler>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEncodeService, EncodeService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IOrganizationsService, OrganizationsService>();
            services.AddScoped<IAbsenceService, AbsenceService>();
            services.AddScoped<IAbsencesService, AbsencesService>();
            services.AddScoped<IEmailService, EmailService>();
        }

        public static void AddCustomCors(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins(config["CorsSettings:DefaultOrigin"])
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        public static void AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UpWork", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                Array.Empty<string>()
                            }
                    });
            });
        }

    }
}
