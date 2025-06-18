using Domain.Contracts;
using E_commerce.Factories;
using E_commerce.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Persistance;
using Persistance.Data;
using Services;
using ServicesAbstractions;
using Shared.SharedTransferObjects.ErrorModels;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddAplicationServices(builder.Configuration);
            builder.Services.AddWebApplicationServices(builder.Configuration);
            
            builder.Services.AddControllers();

            var app = builder.Build();
            await app.InitalizeDBAsync();

            app.useCustomExceptionHandlerMiddleware();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "E commerce App";
                    options.DocExpansion(DocExpansion.None);
                    options.EnableFilter();

                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }  
    }
}
