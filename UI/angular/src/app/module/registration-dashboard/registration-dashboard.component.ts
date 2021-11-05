import { Component, EventEmitter, Output } from '@angular/core';

import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { Registration } from 'src/app/models/auth/registration';

@Component({
  selector: 'app-registration-dashboard',
  templateUrl: './registration-dashboard.component.html',
  styleUrls: ['./registration-dashboard.component.css']
})
export class RegistrationDashboardComponent {

  model: Registration = <Registration>{}
  error: string = '';
  
  @Output() registration = new EventEmitter<Registration>();

  constructor(public activeModal: NgbActiveModal) { }
}
