import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Product, ProductsList } from 'src/app/models';
import { API_URL } from 'src/app/app-injection-tokens';


@Injectable({
  providedIn: 'root'
})
export class ProductHttpService {

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
  ) { }

  getProducts(): Observable<ProductsList> {
    return this._http.get<ProductsList>(`${this._apiUrl}product`);
  }

  getProductById(id: number): Observable<Product> {
    return this._http.get<Product>(`${this._apiUrl}product/` + id);
  }

  getProductByCategory(id: number): Observable<ProductsList> {
    return this._http.get<ProductsList>(`${this._apiUrl}product/category/` + id);
  }
}
