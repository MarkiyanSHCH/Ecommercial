import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { ProductComponent } from './product/product.component';
import { DetailComponent } from './product/detail/detail.component'
import { OrdersComponent } from './components/orders/orders.component';


const routes: Routes = [
{path:'', component:ProductComponent},
{path:'product/:id', component:DetailComponent},
{path:'product/category/:id', component:ProductComponent},
{path: 'orders', component:OrdersComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
