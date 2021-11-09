import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import * as qs from 'qs';
import { Observable } from 'rxjs';

import { API_URL } from 'src/app/app-injection-tokens';
import { ProductsList } from 'src/app/models/product/products';

@Injectable({
  providedIn: 'root'
})
export class CartHttpService {

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
  ) { }

  getCartItem(productIds: number[]): Observable<ProductsList> {
    const queryParams = qs.stringify({
      productIds
    }, {
      encode: false,
      allowDots: true
    });
    return this._http.get<ProductsList>(`${this._apiUrl}cart?${queryParams}`);
  }
}
