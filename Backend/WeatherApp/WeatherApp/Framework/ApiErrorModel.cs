using Newtonsoft.Json;

namespace WeatherApp.Framework
{
    public class ApiErrorModel
    {
        [JsonProperty("errorMessages")]
        public string[] ErrorMessages { get; set; }
    }
}
