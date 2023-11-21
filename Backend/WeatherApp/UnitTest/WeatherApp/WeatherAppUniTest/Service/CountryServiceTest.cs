using System.Collections.Generic;
using WeatherApp.Dto.Location;
using WeatherApp.Framework;
using WeatherApp.Service;
using Xunit;

namespace WeatherAppUniTest.Service
{
    public class CountryServiceTest
    {
        private CountryService countryService;

        public CountryServiceTest()
        {
            this.countryService = new CountryService();
        }

        #region TestMethod

        [Fact]
        public void GetCountries_ReturnListOfCountry()
        {
            var result = this.countryService.GetCountries();

            Assert.IsType<GenericResponse<List<CountryDto>>>(result);
            Assert.True(result.Data.Count > 0);
        }

        #endregion
    }
}
