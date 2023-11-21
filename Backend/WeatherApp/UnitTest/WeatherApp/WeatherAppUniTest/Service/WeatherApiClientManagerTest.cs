using Microsoft.Extensions.Options;
using Moq;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WeatherApp.Dto.Configuration;
using WeatherApp.Dto.Location;
using WeatherApp.Dto.Weather;
using WeatherApp.Framework;
using WeatherApp.Service;
using Xunit;

namespace WeatherAppUniTest.Service
{
    public class WeatherApiClientManagerTest
    {
        private WeatherApiClientManager weatherApiClientManager;
        private Mock<IRestClient> restClient;

        public WeatherApiClientManagerTest()
        {
            IOptions<WeatherApiConfig> options = Options.Create(new WeatherApiConfig() { ApiKey = "key", BaseWeatherUrl ="https://test.weather.com"});
            this.restClient = new Mock<IRestClient>();
            this.weatherApiClientManager = new WeatherApiClientManager(options, this.restClient.Object);
        }

        #region TestMethod

        [Fact]
        public async Task GetWeatherByCityName_Successfull_ReturnWeatherDtoResponse()
        {
            var expectedResponse = new RestResponse
            {
                ResponseStatus = ResponseStatus.Completed,
                StatusCode = HttpStatusCode.OK,
                IsSuccessStatusCode = true,
                Content = "{\"coord\":{\"lon\":115,\"lat\":-8.5},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"base\":\"stations\",\"main\":{\"temp\":304.69,\"feels_like\":310.33,\"temp_min\":304.69,\"temp_max\":304.69,\"pressure\":1009,\"humidity\":64,\"sea_level\":1009,\"grnd_level\":1001},\"visibility\":10000,\"wind\":{\"speed\":4.39,\"deg\":177,\"gust\":4.21},\"clouds\":{\"all\":66},\"dt\":1700541685,\"sys\":{\"type\":1,\"id\":9333,\"country\":\"ID\",\"sunrise\":1700516998,\"sunset\":1700562105},\"timezone\":28800,\"id\":1650535,\"name\":\"Bali\",\"cod\":200}",
            };

            this.restClient.Setup(s => s.ExecuteAsync(It.IsAny<RestRequest>(), default))
                .ReturnsAsync(expectedResponse);

            var result = await weatherApiClientManager.GetWeatherByCityName("cityName");

            Assert.IsType<GenericResponse<WeatherDto>>(result);
            Assert.Equal("Clouds", result.Data.SkyCondition);
        }

        [Fact]
        public async Task GetWeatherByCityName_NotFound_ReturnErrorResponse()
        {
            var expectedResponse = new RestResponse
            {
                ResponseStatus = ResponseStatus.Error,
                StatusCode = HttpStatusCode.NotFound,
                IsSuccessStatusCode = false,
                Content = "{\"cod\":\"404\",\"message\":\"city not found\"}",
            };

            this.restClient.Setup(s => s.ExecuteAsync(It.IsAny<RestRequest>(), default))
                .ReturnsAsync(expectedResponse);

            var result = await weatherApiClientManager.GetWeatherByCityName("cityName");

            Assert.IsType<GenericResponse<WeatherDto>>(result);
            var actualResult = (GenericResponse<WeatherDto>)result;
            Assert.Equal("city not found", actualResult.GetErrorMessage());
        }

        #endregion
    }
}
