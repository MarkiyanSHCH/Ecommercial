import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { API_URL } from 'src/app/app-injection-tokens';
import { CategoryList } from 'src/app/models/category/categories';

@Injectable({
  providedIn: 'root'
})
export class CategoryHttpService {

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
  ) { }

  getCategory(): Observable<CategoryList> {
    return this._http.get<CategoryList>(`${this._apiUrl}category`);
  }
}
