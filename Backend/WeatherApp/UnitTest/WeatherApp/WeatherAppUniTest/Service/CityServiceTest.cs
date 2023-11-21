using System.Collections.Generic;
using WeatherApp.Dto.Location;
using WeatherApp.Framework;
using WeatherApp.Service;
using Xunit;

namespace WeatherAppUniTest.Service
{
    public class CityServiceTest
    {
        private CityService cityService;

        public CityServiceTest()
        {
            this.cityService = new CityService();
        }

        #region TestMethod

        [Fact]
        public void GetCityListByCountryId_CityFoundReturnDtoList()
        {
            var countryId = "ID";

            var result = this.cityService.GetCityListByCountryId(countryId);

            Assert.IsType<GenericResponse<List<CityDto>>>(result);
            Assert.True(result.Data.Count > 0);
        }

        [Fact]
        public void GetCityListByCountryId_CityNotFoundReturnEmptyDtoList()
        {
            var countryId = "FAIL";

            var result = this.cityService.GetCityListByCountryId(countryId);

            Assert.IsType<GenericResponse<List<CityDto>>>(result);
            Assert.True(result.Data.Count == 0);
        }

        #endregion
    }
}
