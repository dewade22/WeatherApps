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
    public class CityControllerTest
    {
        private Mock<ICityService> cityServiceMock;
        private CityController controller;

        public CityControllerTest()
        {
            this.cityServiceMock = new Mock<ICityService>();
            this.controller = new CityController(this.cityServiceMock.Object);
        }

        [Fact]
        public void GetCityByCountryId_Successfull()
        {
            var citiesResponse = new GenericResponse<List<CityDto>>()
            {
                Data = new List<CityDto>(),
            };

            this.cityServiceMock.Setup(s => s.GetCityListByCountryId(It.IsAny<string>()))
                .Returns(citiesResponse);

            var result = this.controller.GetCityByCountryId("country");

            Assert.IsType<OkObjectResult>(result);
            var actualResult = (OkObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
            this.cityServiceMock.Verify(item => item.GetCityListByCountryId(It.IsAny<string>()), Times.Once);
        }
    }
}
