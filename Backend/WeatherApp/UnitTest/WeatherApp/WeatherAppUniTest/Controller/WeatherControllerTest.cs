using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherApp.Controllers;
using WeatherApp.Dto.Weather;
using WeatherApp.Framework;
using WeatherApp.ServiceContract;
using Xunit;

namespace WeatherAppUniTest.Controller
{
    public class WeatherControllerTest
    {
        public const string RandomError = "RandomError";
        private Mock<IWeatherApiClientManager> weatherApiClientManagerMock;
        private WeatherController controller;

        public WeatherControllerTest()
        {
            this.weatherApiClientManagerMock = new Mock<IWeatherApiClientManager>();
            this.controller = new WeatherController(this.weatherApiClientManagerMock.Object);
        }

        [Fact]
        public async void GetWeather_ErrorWhileReadToWeatherAPI_ReturnErrorResponse()
        {
            this.PreparationReadWeather(false);

            var result = await this.controller.GetWeather("cityName");

            Assert.IsType<ObjectResult>(result);
            var actualResult = (ObjectResult)result;
            var actualMessage = ((ApiErrorModel)actualResult.Value).ErrorMessages[0];
            Assert.Equal(RandomError, actualMessage);
            Assert.Equal(400, actualResult.StatusCode);
            this.weatherApiClientManagerMock.Verify(item => item.GetWeatherByCityName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void GetWeather_Success_ReturnOkResult()
        {
            this.PreparationReadWeather(true);

            var result = await this.controller.GetWeather("cityName");

            Assert.IsType<OkObjectResult>(result);
            var actualResult = (OkObjectResult)result;
            Assert.Equal(200, actualResult.StatusCode);
            this.weatherApiClientManagerMock.Verify(item => item.GetWeatherByCityName(It.IsAny<string>()), Times.Once);
        }

        private void PreparationReadWeather(bool valid)
        {
            var response = new GenericResponse<WeatherDto>()
            {
                Data = new WeatherDto(),
            };

            if (!valid)
            {
                response.AddErrorMessage(RandomError, 400);
            }

            this.weatherApiClientManagerMock.Setup(s => s.GetWeatherByCityName(It.IsAny<string>()))
                .ReturnsAsync(response);
        }
    }
}
