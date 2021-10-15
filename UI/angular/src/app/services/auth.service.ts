import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators'

import { API_URL } from '../app-injection-tokens';
import { Token } from '../models/Token';

export const ACCESS_TOKEN_KEY = 'store_access_token'

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  login(email: string, password: string): Observable<any> {
    return this.http.post<Token>(`${this.apiUrl}api/auth/login`, {
      email, password
    }).pipe(
      tap(signInResult => localStorage.setItem(ACCESS_TOKEN_KEY, signInResult.access_token))
    );
  }

  isAuthenticated(): boolean {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return !!(token && !this.jwtHelper.isTokenExpired(token))
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['']);
  }

}
