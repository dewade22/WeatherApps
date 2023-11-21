using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Dto.Weather;
using WeatherApp.Framework;

namespace WeatherApp.ServiceContract
{
    public interface IWeatherApiClientManager
    {
        Task<GenericResponse<WeatherDto>> GetWeatherByCityName(string cityName);
    }
}
