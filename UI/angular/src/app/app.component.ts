import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { CategoryService } from './services/category.service';
import { AuthService } from './services/auth.service';
import { LoginDashboardComponent } from './module/authorization-dashboard/login-dashboard/login-dashboard.component';
import { Login } from './models/Login';
import { Category } from './models/Category';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  categoriesList: Category[] = <Category[]>[];

  constructor(
    private _modalService: NgbModal,
    private _categoryService: CategoryService,
    private _authService: AuthService
  ) { }

  ngOnInit(): void {
    this.refreshProdList();
  }

  public get isLoggedIn(): boolean {
    return this._authService.isAuthenticated()
  }

  login(): void {
    const modalRef = this._modalService.open(LoginDashboardComponent, { centered: true });
    const module = <LoginDashboardComponent>modalRef.componentInstance;
    module.login.subscribe((account: Login) =>
      this._authService.login(account.email, account.password)
        .subscribe(res => module.activeModal.dismiss(),
          error => module.errorMessage("Invalide Email or Password")));
  }

  logout() {
    this._authService.logout()
  }

  refreshProdList() {
    this._categoryService.getCategory()
      .subscribe(data =>
        this.categoriesList = data.categories
      );
  }
}
