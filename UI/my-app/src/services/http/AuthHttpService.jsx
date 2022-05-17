import axios from "axios";
import LocalStorageService from "services/LocaleStorageService";

const localeStorageService = LocalStorageService();

export default function AuthHttpService() {
	const login = ({ email, password }) =>
		axios
			.post(`${process.env.REACT_APP_API_URL}auth/login`, {
				email: email,
				password: password,
			})
			.then(response => {
				localeStorageService.setToken(response.data.access_token);
				localeStorageService.setRefreshToken(response.data.refresh_token);
			});

	const registration = ({ name, email, password }) =>
		axios
			.post(`${process.env.REACT_APP_API_URL}auth/registration`, {
				name: name,
				email: email,
				password: password,
			})
			.then(response => {
				localeStorageService.setToken(response.data.access_token);
				localeStorageService.setRefreshToken(response.data.refresh_token);
			});

	const refreshToken = () =>
		axios
			.post(`${process.env.REACT_APP_API_URL}token/refresh`, {
				token: localeStorageService.getToken(),
				refreshToken: localeStorageService.getRefreshToken(),
			})
			.then(response => {
				localeStorageService.setToken(response.data.access_token);
				localeStorageService.setRefreshToken(response.data.refresh_token);
			});
	return {
		login,
		registration,
		refreshToken,
	};
}
