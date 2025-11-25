using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using Talabat.API.Responses;
using System.Text.Json;

namespace Talabat.API.Middelware
{
    public class ErrorHandlingMiddelware
    {
        private readonly RequestDelegate request;
        private readonly ILogger logger;

        public ErrorHandlingMiddelware(RequestDelegate _request, ILogger _logger)
        {
            request = _request;
            logger = _logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                Console.WriteLine("before middelware");
                await request(context);
                Console.WriteLine("after middelware");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var status = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ServerInternalException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
            var error = new ErrorResponse
            {
                StatusCode = status,
                Message = status == StatusCodes.Status500InternalServerError ? "Internal Server Error" : ex.Message,
                Success = false
            };
            logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;
          
            
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var jsonResponse = JsonSerializer.Serialize(error, options);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
