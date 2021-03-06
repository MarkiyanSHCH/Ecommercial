import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { API_URL } from 'src/app/app-injection-tokens';
import { Order, OrderList, OrderLineList } from 'src/app/models';

@Injectable({
  providedIn: 'root'
})
export class OrderHttpService {

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
  ) { }

  getOrders(): Observable<OrderList> {
    return this._http.get<OrderList>(`${this._apiUrl}orders`);
  }

  getOrderLines(orderId: number): Observable<OrderLineList> {
    return this._http.get<OrderLineList>(`${this._apiUrl}orders/${orderId}/lines`);
  }

  postOrder(request: Order): Observable<number> {
    return this._http.post<number>(`${this._apiUrl}orders`, request)
  }
}
