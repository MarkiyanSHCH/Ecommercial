import { NgModule } from '@angular/core';

import { SharedModule } from 'src/app/shared/shared.module';
import { ProductsListComponent } from './components/products-list/products-list.component';
import { PageComponent } from './page/page.component';
import { PageRoutingModule } from './page-routing.module';

@NgModule({
    declarations: [
        PageComponent,
        ProductsListComponent
    ],
    imports: [
        PageRoutingModule,
        SharedModule
    ]
})
export class PageModule { }
