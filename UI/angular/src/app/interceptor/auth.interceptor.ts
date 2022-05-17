import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

import { ACCESS_TOKEN_KEY, AuthHttpService, AuthService } from '../services';
import { Router } from '@angular/router'; 

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private _authHttpService: AuthHttpService,
    private _authService: AuthService,
    private _router : Router
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const request = req;

    return next.handle(request).pipe(catchError(error => {
      if (!request.url.includes('login') && error.status === 401) {
        return this._authHttpService.refreshToken().pipe(
          switchMap((token: any) => {
            debugger;
            return next.handle(this.updateHeader(request, token));
          }),
          catchError((err) => {
            this._authService.logout();
            return throwError(err);
          })
        );
      }

      return throwError(error);
    }));
  }

  private updateHeader(req: HttpRequest<any>, token: string) {
    req = req.clone({
      headers: req.headers.set("Authorization", `Bearer ${token}`)
    });
    return req;
  }
}
