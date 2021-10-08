import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { STORE_API_URL } from '../app-injection-tokens';
import { ProductsList } from '../models/Products';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private baseApiUrl = `${this.apiUrl}api/`

  constructor(private http: HttpClient, 
    @Inject(STORE_API_URL) private apiUrl: string) { }
  
  getOrders(): Observable<ProductsList>{
    return this.http.get<ProductsList>(`${this.baseApiUrl}orders`)
  }

}
