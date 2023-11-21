using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeatherApp.Framework.Controller;
using WeatherApp.ServiceContract;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("v{version:apiversion}/[controller]")]
    public class CityController : BaseController
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks>Operation to get list of City By Entered Country Id.</remarks>
        /// <param name="countryId">countryId.</param>
        /// <response code="200">Success Response.</response>
        /// <response code="500">Exception thrown.</response>
        [HttpGet]
        [Route("/v{version:apiversion}/city/{countryId}")]
        public IActionResult GetCityByCountryId([FromRoute][Required] string countryId)
        {
            var cityResponse = this.cityService.GetCityListByCountryId(countryId);

            return new OkObjectResult(cityResponse.Data);
        }
    }
}
