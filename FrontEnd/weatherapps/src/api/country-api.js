import BaseApi from "./base-api";

export default class CountryApi extends BaseApi {
    constructor() {
        super("country");
    }

    GetCountries(version) {
        let url = `${this.baseApiUrl}/${version ?? this.defaultVersion}/countries`;

        return this._Get(url);
    }
}