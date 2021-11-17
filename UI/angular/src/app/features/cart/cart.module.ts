import { NgModule } from '@angular/core';

import { ModalWindowModule } from 'src/app/module/modal-window.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CartRoutingModule } from './cart-routing.module';
import { CartComponent } from './pages/cart/cart.component';

@NgModule({
  declarations: [
    CartComponent
  ],
  imports: [
    CartRoutingModule,
    SharedModule,
    ModalWindowModule
  ]
})
export class CartModule { }
