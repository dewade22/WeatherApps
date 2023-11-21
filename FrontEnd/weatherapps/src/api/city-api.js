import BaseApi from "./base-api";

export default class CityApi extends BaseApi {
    constructor() {
        super("city");
    }

    GetCityByCountryId(countryId, version) {
        let url = `${this.baseApiUrl}/${version ?? this.defaultVersion}/${this.baseController}/${countryId}`;

        return this._Get(url);
    }
}