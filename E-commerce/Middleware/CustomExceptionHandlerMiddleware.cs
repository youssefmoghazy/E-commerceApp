using System.Linq.Expressions;
using System.Net;
using System.Text.Json;
using Domain.Exceptions;
using Shared.SharedTransferObjects.ErrorModels;

namespace E_commerce.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, 
            ILogger<CustomExceptionHandlerMiddleware> logger )
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next.Invoke(httpcontext);
                await HandleNotFoundEndPoint(httpcontext);
            }
            catch (Exception ex)
            {
                await HandleExcptionsAsync(httpcontext, ex);
            }
        }

        private async Task HandleExcptionsAsync(HttpContext httpcontext, Exception ex)
        {
            _logger.LogError(ex, "something went wrong");
            //set status code
            //httpcontext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            //set content type
            httpcontext.Response.ContentType = "application/json";
            //Responce object
            var Responce = new ErrorDetails { Message = ex.Message };
            Responce.StatusCode = ex switch
            {
                NoFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetValidationErrors(badRequestException, Responce),
                _ => StatusCodes.Status500InternalServerError
            };
            httpcontext.Response.StatusCode = Responce.StatusCode;
            //return responce as json
            //var JsonResult = JsonSerializer.Serialize(Responce);
            await httpcontext.Response.WriteAsJsonAsync(Responce);
        }

        private int GetValidationErrors(BadRequestException badRequestException, ErrorDetails responce)
        {
            responce.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPoint(HttpContext httpcontext)
        {
            if (httpcontext.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                httpcontext.Response.ContentType = "application/json";
                var Responce = new ErrorDetails
                {
                    Message = $"End Point {httpcontext.Request.Path} Not found",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
                await httpcontext.Response.WriteAsJsonAsync(Responce);
            }
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtention
    {
        public static IApplicationBuilder  useCustomExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            return app;
        }
    }
}
