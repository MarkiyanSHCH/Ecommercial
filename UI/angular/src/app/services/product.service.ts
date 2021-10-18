import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Product } from '../models/Product';
import { API_URL } from '../app-injection-tokens';
import { ProductsList } from '../models/Products';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private _baseApiUrl = `${this._apiUrl}api/`

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
    ) { }

  getProducts(): Observable<ProductsList> {
    return this._http.get<ProductsList>(`${this._baseApiUrl}product`);
  }

  getProductById(id: number): Observable<Product> {
    return this._http.get<Product>(`${this._baseApiUrl}product/` + id);
  }

  getProductByCategory(id: number): Observable<ProductsList> {
    return this._http.get<ProductsList>(`${this._baseApiUrl}product/category/` + id);
  }
}
