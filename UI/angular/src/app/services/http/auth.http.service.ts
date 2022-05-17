import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators'

import { API_URL } from 'src/app/app-injection-tokens';
import { Token } from 'src/app/models';

export const ACCESS_TOKEN_KEY = 'store_access_token';
export const REFRESH_TOKEN_KEY = 'store_refresh_token';

@Injectable({
  providedIn: 'root'
})

export class AuthHttpService {

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
  ) { }

  login(email: string, password: string): Observable<Token> {
    return this._http.post<Token>(`${this._apiUrl}auth/login`, {
      email, password
    }).pipe(
      tap(signInResult => {
        localStorage.setItem(ACCESS_TOKEN_KEY, signInResult.access_token),
        localStorage.setItem(REFRESH_TOKEN_KEY, signInResult.refresh_token)
      })
    );
  }

  registration(name: string, email: string, password: string): Observable<Token> {
    return this._http.post<Token>(`${this._apiUrl}auth/registration`, {
      name, email, password
    }).pipe(
      tap(signInResult => localStorage.setItem(ACCESS_TOKEN_KEY, signInResult.access_token))
    );
  }

  refreshToken(): Observable<Token> {
    let token = localStorage.getItem(ACCESS_TOKEN_KEY);
    let refreshToken = localStorage.getItem(REFRESH_TOKEN_KEY);

    return this._http.post<Token>(`${this._apiUrl}token/refresh`, {
      token, refreshToken
    }).pipe(
      tap(signInResult => {
        localStorage.setItem(ACCESS_TOKEN_KEY, signInResult.access_token),
        localStorage.setItem(REFRESH_TOKEN_KEY, signInResult.refresh_token)
      })
    );
  }
}
