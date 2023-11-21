import BaseApi from "./base-api";

export default class WeatherApi extends BaseApi {
    constructor() {
        super("weather");
    }

    GetWeather(cityName, version) {
        let url = `${this.baseApiUrl}/${version ?? this.defaultVersion}/${this.baseController}/${cityName}`;

        return this._Get(url);
    }
}