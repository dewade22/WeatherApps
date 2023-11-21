namespace WeatherApp.Dto.Configuration
{
    public class WeatherApiConfig
    {
        public const string WeatherApi = "WeatherApi";

        public string ApiKey { get; set; }

        public string BaseWeatherUrl { get; set; }
    }
}
