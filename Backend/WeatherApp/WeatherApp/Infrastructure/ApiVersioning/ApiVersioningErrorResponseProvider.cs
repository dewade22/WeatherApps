using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using WeatherApp.Framework;

namespace WeatherApp.Infrastructure.ApiVersioning
{
    public class ApiVersioningErrorResponseProvider : DefaultErrorResponseProvider
    {
        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            var responseObj = new ApiErrorModel
            {
                ErrorMessages = new string[] { context.Message },
            };

            var response = new ObjectResult(responseObj);
            response.StatusCode = (int)400;

            return response;
        }
    }
}
