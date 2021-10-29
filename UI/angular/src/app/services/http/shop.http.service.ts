import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import * as qs from 'qs';
import { Observable } from 'rxjs';

import { ShopList } from 'src/app/models/shop/shopList';
import { API_URL } from 'src/app/app-injection-tokens';
import { Shop } from 'src/app/models/shop/shop';

@Injectable({
  providedIn: 'root'
})
export class ShopHttpService {

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
  ) { }

  getShops(): Observable<ShopList> {
    return this._http.get<ShopList>(`${this._apiUrl}shop`)
  }

  getShopById(shopId: number): Observable<Shop> {
    const queryParams = qs.stringify({
      shopId
    }, {
      encode: false,
      allowDots: true
    });
    return this._http.get<Shop>(`${this._apiUrl}shop?${queryParams}`);
  }
}
