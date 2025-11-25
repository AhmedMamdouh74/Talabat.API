namespace Talabat.API.Responses
{
    public class ErrorResponse
    {
        bool Success { get; set; } = false;
        string Message { get; set; }= string.Empty;
        int StatusCode { get; set; }

    }
}
