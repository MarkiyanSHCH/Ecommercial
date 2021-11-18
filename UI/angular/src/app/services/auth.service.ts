import { Injectable, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Login, Registration } from 'src/app/models';

import { LoginDashboardComponent } from '../module/login-dashboard/login-dashboard.component';
import { RegistrationDashboardComponent } from '../module/registration-dashboard/registration-dashboard.component';
import { AuthHttpService } from './http/auth.http.service';

const ACCESS_TOKEN_KEY = 'store_access_token'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  isAutorise = new EventEmitter<void>();

  constructor(
    private _modalService: NgbModal,
    private _jwtHelper: JwtHelperService,
    private _router: Router,
    public authHttpService: AuthHttpService
  ) { }

  login(): void {
    const modalRef = this._modalService.open(LoginDashboardComponent, { centered: true });
    const module = <LoginDashboardComponent>modalRef.componentInstance;
    module.login.subscribe((account: Login) =>
      this.authHttpService.login(account.email, account.password)
        .subscribe(() => {
          module.activeModal.dismiss();
          this.isAutorise.next();
        },
          () => module.error = `Invalide Email or Password`));
  }

  registration(): void {
    const modalRef = this._modalService.open(RegistrationDashboardComponent, { centered: true });
    const module = <RegistrationDashboardComponent>modalRef.componentInstance;
    module.registration.subscribe((account: Registration) =>
      this.authHttpService.registration(account.name, account.email, account.password)
        .subscribe(() => {
          module.activeModal.dismiss();
          this.isAutorise.next();
        },
          () => module.error = `User is exist`));
  }

  isAuthenticated(): boolean {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return !!(token && !this._jwtHelper.isTokenExpired(token))
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this._router.navigate(['']);
  }
}
