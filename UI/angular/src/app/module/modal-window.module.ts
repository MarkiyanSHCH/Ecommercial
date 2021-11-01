import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { LoginDashboardComponent } from './login-dashboard/login-dashboard.component';
import { CartModuleComponent } from './cart-dashboard/cart-dashboard.component';
import { OrderDashboardComponent } from './order-dashboard/order-dashboard.component';
import { RegistrationDashboardComponent } from './registration-dashboard/registration-dashboard.component';

@NgModule({
  declarations: [
    LoginDashboardComponent,
    CartModuleComponent,
    OrderDashboardComponent,
    RegistrationDashboardComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    CartModuleComponent,
    LoginDashboardComponent,
    OrderDashboardComponent,
    RegistrationDashboardComponent
  ]
})
export class ModalWindowModule { }
