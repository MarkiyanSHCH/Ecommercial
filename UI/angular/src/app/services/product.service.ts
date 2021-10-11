import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Product } from '../models/Product';
import { STORE_API_URL } from '../app-injection-tokens';
import { ProductsList } from '../models/Products';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private baseApiUrl = `${this.apiUrl}api/`

  constructor(private http: HttpClient, @Inject(STORE_API_URL) private apiUrl: string) { }

  getProducts(): Observable<ProductsList> {
    return this.http.get<ProductsList>(`${this.baseApiUrl}product`);
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseApiUrl}product/` + id);
  }

  getProductByCategory(id: number): Observable<ProductsList> {
    return this.http.get<ProductsList>(`${this.baseApiUrl}product/category/` + id);
  }
}
