import { Component, OnInit } from '@angular/core';

import { CategoryHttpService } from './services/http/category.http.service';
import { Category } from './models/category/category';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  categoriesList: Category[] = <Category[]>[];

  constructor(
    private _categoryHttpService: CategoryHttpService,
    public authService: AuthService
  ) { }

  ngOnInit(): void {
    this._categoryHttpService.getCategory()
      .subscribe(data =>
        this.categoriesList = data.categories
      );
  }
}
