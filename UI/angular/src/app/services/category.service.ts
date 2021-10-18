import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { API_URL } from '../app-injection-tokens';
import { CategoryList } from '../models/Categories';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private baseApiUrl = `${this._apiUrl}api/`

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
    ) { }

  getCategory(): Observable<CategoryList> {
    return this._http.get<CategoryList>(`${this.baseApiUrl}category`);
  }

}
