export const ACCESS_TOKEN_KEY = 'ACCESS_TOKEN_KEY';
export const REFRESH_TOKEN_KEY = 'REFRESH_TOKEN_KEY';

export default function LocalStorageService() {
  const getToken = _ => window.localStorage.getItem(ACCESS_TOKEN_KEY);
  const getRefreshToken = _ => window.localStorage.getItem(REFRESH_TOKEN_KEY);
  const setToken = token => window.localStorage.setItem(ACCESS_TOKEN_KEY, token);
  const setRefreshToken = refreshToken => window.localStorage.setItem(REFRESH_TOKEN_KEY, refreshToken);
  const resetTokens = _ => {
    window.localStorage.removeItem(ACCESS_TOKEN_KEY);
    window.localStorage.removeItem(REFRESH_TOKEN_KEY);
  }
  return{
    getToken,
    getRefreshToken,
    setToken,
    setRefreshToken,
    resetTokens
  };
};


