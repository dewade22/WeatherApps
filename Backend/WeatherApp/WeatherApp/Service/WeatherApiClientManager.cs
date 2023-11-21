using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using WeatherApp.Dto.Configuration;
using WeatherApp.Dto.Weather;
using WeatherApp.Framework;
using WeatherApp.ServiceContract;

namespace WeatherApp.Service
{
    public class WeatherApiClientManager : IWeatherApiClientManager
    {
        private readonly WeatherApiConfig weatherApiConfig;
        private readonly IRestClient restClient;

        public WeatherApiClientManager(
            IOptions<WeatherApiConfig> weatherApiConfig,
            IRestClient restClient)
        {
            this.weatherApiConfig = weatherApiConfig.Value;
            this.restClient = restClient;
        }

        public async Task<GenericResponse<WeatherDto>> GetWeatherByCityName(string cityName)
        {
            var response = new GenericResponse<WeatherDto> ();

            var request = new RestRequest(this.weatherApiConfig.BaseWeatherUrl+"/data/2.5/weather", Method.Get);
            request.AddQueryParameter("q", cityName);
            request.AddQueryParameter("APPID", this.weatherApiConfig.ApiKey);

            var weatherResponse = await this.restClient.ExecuteAsync(request);

            if (!weatherResponse.IsSuccessful)
            {
                var apiResponseContent = JsonConvert.DeserializeObject<dynamic>(weatherResponse.Content);
                var errStatusCode = this.GetErrorStatusCode(apiResponseContent);
                var errMessage = this.GetErrorMessage(apiResponseContent);
                response.AddErrorMessage(errMessage, errStatusCode);
                return response;
            }

            var weathersContent = JsonConvert.DeserializeObject<dynamic>(weatherResponse.Content);

            var dto = new WeatherDto()
            {
                Location = $"lon: {weathersContent.coord.lon} lat: {weathersContent.coord.lat}",
                Time = weathersContent.timezone,
                Wind = weathersContent.wind.speed,
                Visibility = weathersContent.visibility,
                SkyCondition = weathersContent.weather[0].main,
                TemperatureCelcius = this.FahrenheitToCelsius(weathersContent.main.temp.ToObject<decimal>()),
                TemperatureFahrenheit = weathersContent.main.temp,
                DewPoint = 0,
                RelativeHumidity = weathersContent.main.humidity,
                Preasure = weathersContent.main.pressure,
            };

            response.Data = dto;

            return response;
        }

        #region private method

        private string GetErrorMessage(dynamic jsonContent)
        {
            var errorMessage = string.Empty;
            if (jsonContent.message != null)
            {
                errorMessage = jsonContent.message;
            }

            return errorMessage;
        }

        private int GetErrorStatusCode(dynamic jsonContent)
        {
            int statusCode = (int)HttpStatusCode.BadRequest;
            if (jsonContent.cod != null)
            {
                statusCode = jsonContent.cod;
            }

            return statusCode;
        }

        private decimal FahrenheitToCelsius(decimal fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }

        #endregion
    }
}
