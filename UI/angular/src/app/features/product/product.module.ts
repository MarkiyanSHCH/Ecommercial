import { NgModule } from '@angular/core';
import { ModalWindowModule } from 'src/app/module/modal-window.module';

import { SharedModule } from 'src/app/shared/shared.module';
import { DetailComponent } from '../../features/product/pages/detail/detail.component';
import { ProductsComponent } from '../../features/product/pages/products/products.component';
import { ProductRoutingModule } from './product-routing.module';

@NgModule({
  declarations: [
    ProductsComponent,
    DetailComponent
  ],
  imports: [
    ProductRoutingModule,
    SharedModule,
    ModalWindowModule
  ]
})
export class ProductModule { }
