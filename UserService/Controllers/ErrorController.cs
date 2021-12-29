using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserService.CustomException;

namespace UserService.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public void Error()
        {
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception ex = context?.Error;

            HttpContext.Response.ContentType = "application/json";

            int statusCode = 500;
            string message = "Error occurred while processing your request.";

            if (ex is BusinessException businessException)
            {
                statusCode = (int)businessException.StatusCode;
                message = businessException.Message;
            }
            else if (ex != null)
            {
                statusCode = Response.StatusCode;
                message = ex.Message;
            }

            HttpContext.Response.StatusCode = statusCode;
            HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { message, statusCode }));
        }

        [Route("error-development")]
        public void ErrorDevelopment()
        {
            IExceptionHandlerPathFeature context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            Exception ex = context?.Error;

            HttpContext.Response.ContentType = "application/json";

            int statusCode = Response.StatusCode;
            string message = "Error occurred while processing your request.";

            if (ex is BusinessException businessException)
            {
                statusCode = (int)businessException.StatusCode;
                message = businessException.Message;
            }

            HttpContext.Response.StatusCode = statusCode;

            if (ex != null)
            {
                HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    context.Path,
                    ex.Message,
                    StatusCode = statusCode,
                    ex.StackTrace,
                    ex.InnerException
                }));
            }
            else
            {
                HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { message, statusCode }));
            }
        }
    }
}
