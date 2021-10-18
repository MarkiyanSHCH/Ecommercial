import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Login } from 'src/app/models/Login';

@Component({
  selector: 'app-login-dashboard',
  templateUrl: './login-dashboard.component.html',
  styleUrls: ['./login-dashboard.component.css']
})
export class LoginDashboardComponent implements OnInit {

  model: Login = <Login>{};
  error: string = '';

  @Output() login = new EventEmitter<Login>();

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  onLoginClicked(): void {
    this.login.next(this.model);
  }

  errorMessage(err: string): void {
    this.error = err;
  }

}
