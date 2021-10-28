import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ShopList } from 'src/app/models/shop/shopList';
import { API_URL } from 'src/app/app-injection-tokens';

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
}
