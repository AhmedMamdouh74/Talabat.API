using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Responses;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public abstract class BaseAPIController : ControllerBase
    {
        protected ActionResult<T> Success<T>(T data, string message = "Request was successful.")
            => Ok(ApiResponse<T>.CreateSuccessResponse(data, message));
        protected ActionResult Error(string message, int statusCode = 400)
        {
            var errorResponse = new ErrorResponse
            {
                Message = message,
                StatusCode = statusCode,
                Success = false
            };
            return StatusCode(statusCode, errorResponse);
        }
    }
}
