import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Category } from '../models/Category';
import { API_URL } from '../app-injection-tokens';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private baseApiUrl = `${this.apiUrl}api/`

  constructor(private http: HttpClient, @Inject(API_URL) private apiUrl: string) { }

  getCategory(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.baseApiUrl}category`);
  }

}
