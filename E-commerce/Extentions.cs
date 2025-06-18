using System.ComponentModel.DataAnnotations;
using System.Text;
using Domain.Contracts;
using E_commerce.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.SharedTransferObjects.Authentication;

namespace E_commerce;

public static class Extentions
{
    public static IServiceCollection AddSwagerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                Description = "JWT Authorization header using Bearer Scheme. \r\n\r\n Enter 'Bearer' [space] and then your token "
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new List<string>()
                }
            });
        });

        return services;
    }
    public static IServiceCollection AddWebApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateAPIValidationResponse);
        services.AddSwagerServices();
        ConfigureJwt(services, configuration);
        return services;
    }
    internal static async Task<WebApplication> InitalizeDBAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbInitalizer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await dbInitalizer.InitializeAsync();
        await dbInitalizer.InitializeIdentityAsync();
        return app;
    }

    private static void ConfigureJwt(IServiceCollection services, IConfiguration configuration)
    {
        var jwt = configuration.GetSection("JWTOptions").Get<JWTOptions>();
        services.AddAuthentication(configureOptions: config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(configureOptions: config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt!.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(jwt.SecretKey))
                };
            });
    }
}
