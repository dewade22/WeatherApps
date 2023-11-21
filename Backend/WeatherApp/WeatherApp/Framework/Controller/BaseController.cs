using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Framework.Controller
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult GetApiError(string errorMessage, int? httpStatusCode = null)
        {
            var messages = new string[] { errorMessage };
            return GetApiError(messages, httpStatusCode);
        }

        protected IActionResult GetApiError(string[] errorMessages, int? httpStatusCode = null)
        {
            var actualStatusCode = httpStatusCode.HasValue ? httpStatusCode.Value : 400;
            var responseObj = new ApiErrorModel
            {
                ErrorMessages = errorMessages,
            };

            return this.StatusCode(actualStatusCode, responseObj);
        }
    }
}
