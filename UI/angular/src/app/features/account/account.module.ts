import { NgModule } from '@angular/core';

import { SharedModule } from 'src/app/shared/shared.module';
import { AccountRoutingModule } from './account-routing.module';

import {
  OrderCollapseComponent,
  OrderLineComponent
} from './components';

import {
  AccountComponent,
  AccountInfoComponent,
  ChangePasswordComponent,
  OrdersComponent,
  WebsiteInfoComponent,
  WebsiteInfoEditComponent
} from './pages';

@NgModule({
  declarations: [
    AccountComponent,
    AccountInfoComponent,
    WebsiteInfoComponent,
    WebsiteInfoEditComponent,
    OrdersComponent,
    OrderCollapseComponent,
    OrderLineComponent,
    ChangePasswordComponent
  ],
  imports: [
    SharedModule,
    AccountRoutingModule
  ]
})
export class AccountModule { }
