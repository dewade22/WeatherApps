using System.Collections.Generic;
using WeatherApp.Dto.Location;
using WeatherApp.Framework;
using WeatherApp.ServiceContract;

namespace WeatherApp.Service
{
    public class CountryService : ICountryService
    {
        public CountryService()
        {
        }

        public GenericResponse<List<CountryDto>> GetCountries()
        {
            var respone = new GenericResponse<List<CountryDto>>()
            {
                Data = new List<CountryDto>(),
            };

            var countries = this.PopulateCountries();

            foreach (var country in countries)
            {
                var countryDto = new CountryDto()
                {
                    Id = country.Id,
                    Name = country.Name,
                };

                respone.Data.Add(countryDto);
            }

            return respone;
        }

        private List<CountryDto> PopulateCountries()
        {
            var countryList = new List<CountryDto>();

            countryList.Add(new CountryDto { Id = "ID", Name = "Indonesia" });
            countryList.Add(new CountryDto { Id = "UK", Name = "United Kingdom" });
            countryList.Add(new CountryDto { Id = "AUS", Name = "Australia" });

            return countryList;
        }
    }
}
