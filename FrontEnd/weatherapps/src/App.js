import React, { useState, useEffect } from "react";
import { toast } from "react-toastify";
import "./App.css";
import { CityApiService, CountryApiService, WeatherApiService } from "./api/shared-api";

const cityService = new CityApiService();
const countryService = new CountryApiService();
const weatherApi = new WeatherApiService();

function App() {
  const defaultCityOption = { id: "", name: "Select City" };
  const [isLoading, setIsLoading] = useState(false);
  const [countries, setCountries] = useState();
  const [cities, setCities] = useState([defaultCityOption]);
  const [selectedCountry, setSelectedCountry] = useState("");
  const [selectedCity, setSelectedCity] = useState("");
  const [loadingWeather, setLoadingWeather] = useState(false);
  const [weather, setWeather] = useState();

  useEffect(() => {
    if (!countries && !isLoading){
      setIsLoading(true);
      getCountries();
    }
  }, []);

  const onNetworkCallError = (err) => {
    setIsLoading(false);
    if (err.response && err.response.data && err.response.data.errorMessages) {
      err.response.data.errorMessages.forEach((v) =>
        toast.error(err.response.data.errorMessages[0])
      );
      return;
    }

    toast.error(err.message);
  };

  const getCountries = () => {
    countryService
      .GetCountries()
      .then((res) => {
        setIsLoading(false);
        setCountries([{ id: "", name: "Select Country" }, ...res.data]);
      })
      .catch(onNetworkCallError);
  };

  const getCities = (countryId) => {
    setSelectedCity("");
    if (countryId === "") {
      setCities([defaultCityOption]);
      return;
    }

    cityService
      .GetCityByCountryId(countryId)
        .then((res) => {
          setCities([defaultCityOption, ...res.data])
        })
        .catch(onNetworkCallError);
  };

  const showWeather = () => {
    setLoadingWeather(true);
    weatherApi
      .GetWeather(selectedCity)
        .then((res) => {
          setWeather(res.data);
          setLoadingWeather(false);
        })
        .catch(onNetworkCallError);
  };

  if (isLoading) {
    return(<h1 className="loading">Loading Content</h1>);
  }

  return (
    <div className="App">
      <div className="body">
        <h1>Weather Apps</h1>
        <div className="section-wrapper">
          <label>Select Country</label>
          <select
            id="country"
            name="country"
            value={selectedCountry}
            onChange={(e) => {
              setSelectedCountry(e.target.value);
              getCities(e.target.value);
            }}>
            {countries &&
              countries.map((item, key) => {
                return (
                  <option key={key} value={item.id}>
                    {item.name}
                  </option>
                );
              })}
          </select>
        </div>
        <div className="section-wrapper">
          {selectedCountry &&
          <>
            <label>Select City</label>
            <select
              id="city"
              name="city"
              value={selectedCity}
              onChange={(e) => {
                setSelectedCity(e.target.value);
                setWeather();
              }}>
              {cities &&
                cities.map((item, key) => {
                  return (
                    <option key={key} value={item.name}>
                      {item.name}
                    </option>
                  );
                })}
            </select>
          </>}
        </div>
        <div className="section-wrapper">
          {(selectedCity !== "Select City" && selectedCity !== "") && <button onClick={(e) => showWeather()} disabled={loadingWeather}>Show Weather</button>}
        </div>
        <hr/>
        <div className="section-weather">
          { loadingWeather ?
            <label>loading data ...</label> : weather ?
            (
              <>
                <label>Location : {weather.location}</label>
                <label>Time : {weather.time}</label>
                <label>Wind : {weather.wind}</label>
                <label>Visibility : {weather.visibility}</label>
                <label>Sky Condition : {weather.skyCondition}</label>
                <label>Temperature (C) : {weather.temperatureCelcius}</label>
                <label>Temperature (F) : {weather.temperatureFahrenheit}</label>
                <label>DewPoint : {weather.dewPoint }</label>Preasure
                <label>Relative Humidity : {weather.relativeHumidity }</label>
                <label>Preasure : {weather.preasure }</label>
              </>
            ) :
            <></>
            }
        </div>
      </div>
    </div>
  );
}

export default App;
