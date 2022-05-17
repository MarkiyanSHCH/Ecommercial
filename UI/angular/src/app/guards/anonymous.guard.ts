import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { ACCESS_TOKEN_KEY, AuthHttpService } from '../services';

import { AuthService } from '../services/auth.service';

@Injectable()
export class AnonymousGuard implements CanActivate {

  constructor(
    private _authService: AuthService,
    private _router: Router,
    private _authHttpService: AuthHttpService
  ) { }

  canActivate(): Observable<boolean> | boolean {
    if (this._authService.isAuthenticated()) return true;

    this._authService.login();

    this._router.navigate(['/']);

    return false;
  }
}
