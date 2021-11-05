import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators'

import { API_URL } from 'src/app/app-injection-tokens';
import { Login } from 'src/app/models/auth/login';
import { Registration } from 'src/app/models/auth/registration';
import { Token } from 'src/app/models/auth/token';

export const ACCESS_TOKEN_KEY = 'store_access_token'

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
      tap(signInResult => localStorage.setItem(ACCESS_TOKEN_KEY, signInResult.access_token))
    );
  }

  registration(name: string, email: string, password: string): Observable<Token> {
    return this._http.post<Token>(`${this._apiUrl}auth/registration`, {
      name, email, password
    }).pipe(
      tap(signInResult => localStorage.setItem(ACCESS_TOKEN_KEY, signInResult.access_token))
    );
  }
}
