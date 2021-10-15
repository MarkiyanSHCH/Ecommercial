import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { LoginDashboardComponent } from './login-dashboard/login-dashboard.component';



@NgModule({
  declarations: [
    LoginDashboardComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    LoginDashboardComponent
  ]
})
export class AuthorizationDashboardModule { }
