using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using UpWork.Api.Requirements;
using UpWork.Api.Requirements.Handlers;
using UpWork.Common.Dto;
using UpWork.Common.DTO;
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
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEncodeService, EncodeService>();
            services.AddScoped<IPermissionsService, PermissionsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IOrganizationsService, OrganizationsService>();
            services.AddSingleton<IAuthorizationHandler, MatchOrganizationQueryHandler>();
            services.AddSingleton<IAuthorizationHandler, MatchOrganizationBodyHandler>();

        }

        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}
