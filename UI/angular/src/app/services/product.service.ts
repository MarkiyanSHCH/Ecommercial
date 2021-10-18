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

  private baseApiUrl = `${this._apiUrl}api/`

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
    ) { }

  getProducts(): Observable<ProductsList> {
    return this._http.get<ProductsList>(`${this.baseApiUrl}product`);
  }

  getProductById(id: number): Observable<Product> {
    return this._http.get<Product>(`${this.baseApiUrl}product/` + id);
  }

  getProductByCategory(id: number): Observable<ProductsList> {
    return this._http.get<ProductsList>(`${this.baseApiUrl}product/category/` + id);
  }

}
