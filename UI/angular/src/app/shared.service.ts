import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { Product } from './models/Product';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly APIUrl = "http://localhost:51450/api";
  //readonly PhotoUrl

  constructor(private http: HttpClient) {
    }

  getProducts():Observable<Product[]> {
      return this.http.get<Product[]>(this.APIUrl + '/product');
  }
   
  getProductById(id: number):Observable<Product> {
      return this.http.get<Product>(this.APIUrl + '/product/' + id);
  }

  getProductByCategory(id: number):Observable<Product[]> {
    return this.http.get<Product[]>(this.APIUrl + '/product/category/' + id);
  }

  getCategory():Observable<any[]>{
    return this.http.get<any[]>(this.APIUrl + '/category/');
  }
}
