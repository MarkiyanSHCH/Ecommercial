import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { STORE_API_URL } from '../app-injection-tokens';
import { Product } from '../models/Product';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private baseApiUrl = `${this.apiUrl}api/`
  constructor(private http: HttpClient, @Inject(STORE_API_URL) private apiUrl: string) { }
  getOrders(): Observable<Product[]>{
    return this.http.get<Product[]>(`${this.baseApiUrl}orders`)
  }
}
