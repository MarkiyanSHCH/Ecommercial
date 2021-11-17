import { Component, OnInit } from '@angular/core';

import { finalize } from 'rxjs/operators';

import { Loadable, Profile } from 'src/app/models';
import { ProfileHttpService } from 'src/app/services';

@Component({
  selector: 'app-website-info',
  templateUrl: './website-info.component.html',
  styleUrls: ['./website-info.component.css']
})
export class WebsiteInfoComponent extends Loadable implements OnInit {

  profile: Profile = <Profile>{};

  constructor(
    private _profileHttpService: ProfileHttpService
  ) {
    super();
  }

  ngOnInit() {
    this.enableLoading();
    this._profileHttpService
      .getProfile()
      .pipe(finalize(() => this.disableLoading()))
      .subscribe(res => this.profile = res);
  }
}
