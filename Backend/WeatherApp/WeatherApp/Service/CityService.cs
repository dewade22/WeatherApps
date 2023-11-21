using System.Collections.Generic;
using System.Linq;
using WeatherApp.Dto.Location;
using WeatherApp.Framework;
using WeatherApp.ServiceContract;

namespace WeatherApp.Service
{
    public class CityService : ICityService
    {
        public CityService()
        {
        }

        public GenericResponse<List<CityDto>> GetCityListByCountryId(string countryId)
        {
            var respone = new GenericResponse<List<CityDto>>()
            {
                Data = new List<CityDto>(),
            };

            var cities = this.PopulateCities(countryId);

            foreach (var city in cities)
            {
                var cityDto = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    CountryId = city.CountryId,
                };

                respone.Data.Add(cityDto);
            }

            return respone;
        }

        #region PrivateMethod

        private List<CityDto> PopulateCities(string countryId = "")
        {
            var cities = new List<CityDto>();

            cities.Add(new CityDto { Id = "JKT", Name = "Jakarta", CountryId = "ID" });
            cities.Add(new CityDto { Id = "BLI", Name = "Bali", CountryId = "ID" });
            cities.Add(new CityDto { Id = "JMB", Name = "Jember", CountryId = "ID" });
            cities.Add(new CityDto { Id = "JPR", Name = "Jepara", CountryId = "ID" });
            cities.Add(new CityDto { Id = "LDN", Name = "London", CountryId = "UK" });
            cities.Add(new CityDto { Id = "MCS", Name = "Manchester", CountryId = "UK" });
            cities.Add(new CityDto { Id = "LVP", Name = "Liverpool", CountryId = "UK" });
            cities.Add(new CityDto { Id = "MLB", Name = "Melbourne", CountryId = "AUS" });
            cities.Add(new CityDto { Id = "SYD", Name = "Sydney", CountryId = "AUS" });
            cities.Add(new CityDto { Id = "PRT", Name = "Perth", CountryId = "AUS" });

            if (string.IsNullOrEmpty(countryId))
            {
                return cities;
            }

            return cities.Where(item => item.CountryId.ToLowerInvariant() == countryId.ToLowerInvariant()).ToList();
        }

        #endregion
    }
}
