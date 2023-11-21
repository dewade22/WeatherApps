import axios from "axios";

export default class BaseApi {
    constructor(baseController, defaultVersion) {
        this.baseApiUrl = process.env.REACT_APP_API_URL;
        this.baseController = baseController;
        this.defaultVersion = defaultVersion ?? "v1";
    }

    _Get(url) {
        return axios.get(url)
    };
}