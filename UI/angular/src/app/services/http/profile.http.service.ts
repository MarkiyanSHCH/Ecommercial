import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { API_URL } from 'src/app/app-injection-tokens';
import {
  ChangePassword,
  Profile,
  ProfileName
} from 'src/app/models';

@Injectable({
  providedIn: 'root'
})
export class ProfileHttpService {

  constructor(
    private _http: HttpClient,
    @Inject(API_URL) private _apiUrl: string
  ) { }

  getProfile(): Observable<Profile> {
    return this._http.get<Profile>(`${this._apiUrl}profile`);
  }

  updateProfile(name: ProfileName): Observable<string> {
    return this._http.patch<string>(`${this._apiUrl}profile/edit/name`, name);
  }

  updatePassword(changePassword: ChangePassword): Observable<ProfileName> {
    return this._http.patch<ProfileName>(`${this._apiUrl}profile/edit/password`, changePassword);
  }
}
