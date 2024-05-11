using System.Text;

using Infrastructure;

using Application;
using Application.Interfaces;

using Domain.Configurations;
using Presentation.Middlewares;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddApplicationService();

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDatabaseContexts(configuration);

builder.Services.Configure<TokenConfiguration>(
    configuration.GetSection("TokenConfiguration"));

var tokenConfiguration = configuration.GetSection("TokenConfiguration")
    .Get<TokenConfiguration>();

builder.Services.AddCors(
    option => option.AddDefaultPolicy(
        builder => builder
            .AllowAnyMethod().AllowAnyHeader()
            .AllowCredentials().SetIsOriginAllowed(origin => true)));

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = false;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfiguration.Issuer,
        ValidAudience = tokenConfiguration.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(tokenConfiguration.Secret)),
    };
});
var app = builder.Build();

app.UseRouting();

app.UseCors();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
