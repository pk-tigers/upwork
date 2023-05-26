using Microsoft.EntityFrameworkCore;
using UpWork.Api.Extensions;
using UpWork.Api.Filters;
using UpWork.Database;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddCustomServices(config);
builder.Services.AddCustomDbContext(config);
builder.Services.AddCustomAuth(config);
builder.Services.AddCustomCors(config);
builder.Services.AddCustomSwaggerGen();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(UnauthorizedExceptionFilterAttribute));
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

Configure(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();


static void Configure(IApplicationBuilder app)
{
    using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
    var dbServ = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    dbServ?.Database.Migrate();
}