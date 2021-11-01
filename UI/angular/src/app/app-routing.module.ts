import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DetailComponent } from './components//products/detail/detail.component';
import { ProductsComponent } from './components/products/products.component';
import { OrdersComponent } from './components/orders/orders.component';
import { CartComponent } from './components/cart/cart.component';

const routes: Routes = [
  { path: '', component: ProductsComponent },
  { path: 'product/:id', component: DetailComponent },
  { path: 'category/:id', component: ProductsComponent },
  { path: 'order', component: OrdersComponent },
  { path: 'cart', component: CartComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
