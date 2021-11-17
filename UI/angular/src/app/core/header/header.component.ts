import { Component, OnInit } from '@angular/core';

import { Category } from 'src/app/models/category/category';
import { AuthService } from 'src/app/services/auth.service';
import { CategoryHttpService } from 'src/app/services/http/category.http.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  categoriesList: Category[] = <Category[]>[];

  constructor(
    private _categoryHttpService: CategoryHttpService,
    public authService: AuthService
  ) { }

  ngOnInit() {
    this._categoryHttpService.getCategory()
      .subscribe(data =>
        this.categoriesList = data.categories
      );
  }

}
