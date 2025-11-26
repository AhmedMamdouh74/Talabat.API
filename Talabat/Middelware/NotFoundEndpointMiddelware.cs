using Microsoft.AspNetCore.Http;

namespace Talabat.API.Middelware
{
    public class NotFoundEndpointMiddelware
    {
        private readonly RequestDelegate request;

        public NotFoundEndpointMiddelware(RequestDelegate _request)
        {
            request = _request;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("before middelware from not found");
            await request(context);
            Console.WriteLine("after middelware from not found");
            if (context.Response.StatusCode == StatusCodes.Status404NotFound&&!context.Response.HasStarted)
            {
                var errorResponse = new
                {
                    message = "The requested endpoint was not found.",
                    statusCode = StatusCodes.Status404NotFound,
                    success = false
                };
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
