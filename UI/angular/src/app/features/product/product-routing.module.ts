import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DetailComponent } from '../../features/product/pages/detail/detail.component';
import { ProductsComponent } from '../../features/product/pages/products/products.component';

const routes: Routes = [
    {
        path: '',
        component: ProductsComponent,
    },
    {
        path: 'product/:id',
        component: DetailComponent
    },
    {
        path: 'category/:id',
        component: ProductsComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProductRoutingModule { }
