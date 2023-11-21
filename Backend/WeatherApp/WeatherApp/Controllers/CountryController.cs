using Microsoft.AspNetCore.Mvc;
using WeatherApp.Framework.Controller;
using WeatherApp.ServiceContract;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("v{version:apiversion}/[controller]")]
    public class CountryController : BaseController
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }


        /// <summary>
        ///
        /// </summary>
        /// <remarks>Operation to get list of country.</remarks>
        /// <response code="200">Success Response.</response>
        /// <response code="500">Exception thrown.</response>
        [HttpGet]
        [Route("/v{version:apiversion}/countries")]
        public IActionResult GetCountries()
        {
            var countriesResponse = this.countryService.GetCountries();

            return new OkObjectResult(countriesResponse.Data);
        }
    }
}
