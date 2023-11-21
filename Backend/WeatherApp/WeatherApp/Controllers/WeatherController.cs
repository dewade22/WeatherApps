using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WeatherApp.Framework.Controller;
using WeatherApp.ServiceContract;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("v{version:apiversion}/[controller]")]
    public class WeatherController : BaseController
    {
        private readonly IWeatherApiClientManager weatherApiClientManager;
        public WeatherController(IWeatherApiClientManager weatherApiClientManager)
        {
            this.weatherApiClientManager = weatherApiClientManager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>Read Weather by city name.</remarks>
        /// <param name="cityName">City Name.</param>
        /// <response code="200">Success Response.</response>
        /// <response code="400">Some Error While requesting Data.</response>
        /// <response code="401">Api Key is Incorrect.</response>
        /// <response code="404">Data Not Found.</response>
        /// <response code="500">Exception thrown.</response>
        [HttpGet]
        [Route("/v{version:apiversion}/weather/{cityName}")]
        public async Task<IActionResult> GetWeather([FromRoute][Required] string cityName)
        {
            var weatherResponse = await this.weatherApiClientManager.GetWeatherByCityName(cityName);
            if (weatherResponse.IsError())
            {
                return this.GetApiError(weatherResponse.GetMessageErrorTextArray(), weatherResponse.GetErrorStatusCode());
            }

            return new OkObjectResult(weatherResponse.Data);
        }
    }
}
