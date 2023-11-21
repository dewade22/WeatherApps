﻿using System.Collections.Generic;
using WeatherApp.Dto.Location;
using WeatherApp.Framework;

namespace WeatherApp.ServiceContract
{
    public interface ICountryService
    {
        GenericResponse<List<CountryDto>> GetCountries();
    }
}
