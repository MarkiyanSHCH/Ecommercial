import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { Loadable, Profile, ProfileName } from 'src/app/models';
import { ProfileHttpService } from 'src/app/services';

@Component({
  selector: 'app-website-info-edit',
  templateUrl: './website-info-edit.component.html',
  styleUrls: ['./website-info-edit.component.css']
})
export class WebsiteInfoEditComponent extends Loadable implements OnInit {

  profile: Profile = <Profile>{};

  constructor(
    private _profileHttpService: ProfileHttpService,
    private _route: ActivatedRoute,
    private _router: Router,
    public location: Location
  ) {
    super();
  }

  ngOnInit() {
    this.enableLoading();
    const profile = window.history.state.profile as Profile;
    let profile$: Observable<Profile>;

    if (profile) profile$ = of(profile);
    else profile$ = this._profileHttpService.getProfile();

    profile$
      .pipe(finalize(() => this.disableLoading()))
      .subscribe((p: Profile) => this.profile = p);
  }

  onUserEditSubmit(): void {
    this.enableLoading();
    this._profileHttpService
      .updateProfile(<ProfileName>{ name: this.profile.name })
      .pipe(finalize(() => this.disableLoading()))
      .subscribe(() =>
        this._router
          .navigate(['../'], { relativeTo: this._route })
      );
  }
}
