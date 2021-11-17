import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

import { finalize } from 'rxjs/operators';

import {
  ActionState,
  ChangePassword,
  Loadable,
  ProblemDetails
} from 'src/app/models';
import { ProfileHttpService } from 'src/app/services';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent extends Loadable {

  ActionState = ActionState;
  state: ActionState = ActionState.Initial;

  error: ProblemDetails = <ProblemDetails>{};
  changePasswordModel: ChangePassword = <ChangePassword>{};

  constructor(
    private _profileHttpService: ProfileHttpService,
    private _route: ActivatedRoute,
    private _router: Router,
    public location: Location
  ) {
    super();
  }

  onChangePasswordSubmit(): void {
    this.enableLoading();
    this._profileHttpService
      .updatePassword(this.changePasswordModel)
      .pipe(finalize(() => this.disableLoading()))
      .subscribe(
        () => this._router.navigate(['../'], { relativeTo: this._route }),
        (errorResponse: HttpErrorResponse) => {
          this.state = ActionState.Failed;
          this.error = errorResponse.error;
        }
      );
  }
}
