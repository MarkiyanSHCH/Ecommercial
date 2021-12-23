import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PageComponent } from './page/page.component';
import { ProductsListComponent } from './components/products-list/products-list.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { OrderListComponent } from './components/order-list/order-list.component';

const routes: Routes = [
    {
        path: '',
        component: PageComponent,
        children: [
            {
                path: 'product',
                component: ProductsListComponent
            },
            {
                path: 'category',
                component: CategoryListComponent
            },
            {
                path: 'order',
                component: OrderListComponent
            }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PageRoutingModule { }
