import { Component, OnInit } from '@angular/core';

import { CategoryService } from './services/category.service';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = ""
  CategoriesList: any = [];

  constructor(private _categoryService: CategoryService,
    private _authService: AuthService) { }

  ngOnInit(): void {
    this.refreshProdList();
  }

  public get isLoggedIn(): boolean {
    return this._authService.isAuthenticated()
  }

  login(email: string, password: string) {
    this._authService.login(email, password)
      .subscribe(res => {

      }, error => {
        alert("Wrong login or password")
      })
  }

  logout() {
    this._authService.logout()
  }

  refreshProdList() {
    this._categoryService.getCategory().subscribe(data => {
      this.CategoriesList = data;
    });
  }

}
