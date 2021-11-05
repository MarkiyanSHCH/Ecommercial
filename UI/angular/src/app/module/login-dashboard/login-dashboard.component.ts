import { Component, EventEmitter, Output } from '@angular/core';

import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { Login } from 'src/app/models/auth/login';

@Component({
  selector: 'app-login-dashboard',
  templateUrl: './login-dashboard.component.html',
  styleUrls: ['./login-dashboard.component.css']
})
export class LoginDashboardComponent {

  model: Login = <Login>{};
  error: string = '';

  @Output() login = new EventEmitter<Login>();

  constructor(public activeModal: NgbActiveModal) { }
}
