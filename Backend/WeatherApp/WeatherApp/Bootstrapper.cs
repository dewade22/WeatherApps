using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using WeatherApp.Service;
using WeatherApp.ServiceContract;

namespace WeatherApp
{
    public class Bootstrapper
    {
        public static void SetupServices(IServiceCollection service)
        {
            service.AddScoped<ICityService, CityService>();
            service.AddScoped<ICountryService, CountryService>();
            service.AddScoped<IWeatherApiClientManager, WeatherApiClientManager>();
            service.AddScoped<IRestClient, RestClient>();
        }
    }
}
