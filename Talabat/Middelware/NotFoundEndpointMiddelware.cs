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
            await request(context);
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
