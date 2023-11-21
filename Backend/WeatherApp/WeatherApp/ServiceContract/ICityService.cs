using System.Collections.Generic;
using WeatherApp.Dto.Location;
using WeatherApp.Framework;

namespace WeatherApp.ServiceContract
{
    public interface ICityService
    {
        GenericResponse<List<CityDto>> GetCityListByCountryId(string countryId);
    }
}
