import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly APIUrl = "http://localhost:5000/api";
  //readonly PhotoUrl

  constructor(private http: HttpClient) {
    }

  getProducts():Observable<any[]> {
      return this.http.get<any>(this.APIUrl + '/product');
  }
   
  getProductById(id: number): Promise<any> {
      return this.http.get(this.APIUrl + '/product/' + id).toPromise();
  }

  getProductByCategory(id: number):Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/product/category/' + id);
  }

  getCategory():Observable<any[]>{
    return this.http.get<any>(this.APIUrl + '/category/');
  }
}
