import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { API_URL } from '../app-injection-tokens';
import { ProductsList } from '../models/Products';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private _baseApiUrl = `${this._apiUrl}api/`

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
  ) { }

  getOrders(): Observable<ProductsList> {
    return this._http.get<ProductsList>(`${this._baseApiUrl}orders`)
  }
}
