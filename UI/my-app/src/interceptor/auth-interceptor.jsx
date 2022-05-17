import axios from "axios";
import AuthHttpService from "services/http/AuthHttpService";
import LocalStorageService from "services/LocaleStorageService";

const localeStorageService = LocalStorageService();
const authHttpService = AuthHttpService();

const AuthInterceptor = () => {
	axios.interceptors.request.use(
		request => {
			const token = localeStorageService.getToken();
			const isApiUrl = request.url.startsWith(process.env.REACT_APP_API_URL);
			const isDefaultRelativeApiUrl = request.url.startsWith("/");
			if (token && (isApiUrl || isDefaultRelativeApiUrl))
				request.headers.common.Authorization = `Bearer ${token}`;
			return request;
		},
		error => Promise.reject(error)
	);

	axios.interceptors.response.use(
		response => response,
		error => {
			if (error.response && error.response.status === 401) {
				return authHttpService
					.refreshToken()
					.then(response => {
						error.config.headers.Authorization = `Bearer ${response.data.access_token}`;
						return axios.request(error.config);
					})
					.catch(_ => {
						localeStorageService.resetTokens();
						window.location = "/auth/login";
					});
			}
			return Promise.reject(error);
		}
	);
};

export default AuthInterceptor;
