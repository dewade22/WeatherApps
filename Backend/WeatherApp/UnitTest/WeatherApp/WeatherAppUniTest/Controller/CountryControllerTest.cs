using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using WeatherApp.Controllers;
using WeatherApp.Dto.Location;
using WeatherApp.Framework;
using WeatherApp.ServiceContract;
using Xunit;

namespace WeatherAppUniTest.Controller
{
    public class CountryControllerTest
    {
        private Mock<ICountryService> countryServiceMock;
        private CountryController countryController;

        public CountryControllerTest()
        {
            this.countryServiceMock = new Mock<ICountryService>();
            this.countryController = new CountryController(this.countryServiceMock.Object);
        }

        [Fact]
        public void GetCountryList_Successfull()
        {
            var countriesResponse = new GenericResponse<List<CountryDto>>()
            {
                Data = new List<CountryDto>(),
            };

            this.countryServiceMock.Setup(s => s.GetCountries())
                .Returns(countriesResponse);

            var result = this.countryController.GetCountries();

            Assert.IsType<OkObjectResult>(result);
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            this.countryServiceMock.Verify(item => item.GetCountries(), Times.Once);
        }
    }
}
