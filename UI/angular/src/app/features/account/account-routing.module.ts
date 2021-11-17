import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {
    AccountComponent,
    AccountInfoComponent,
    ChangePasswordComponent,
    OrdersComponent,
    WebsiteInfoComponent,
    WebsiteInfoEditComponent
} from './pages';

const routes: Routes = [
    {
        path: '',
        component: AccountComponent,
        children: [
            {
                path: '',
                redirectTo: 'account-info',
                pathMatch: 'full'
            },
            {
                path: 'account-info',
                data: { group: 'Account', name: 'Account' },
                component: AccountInfoComponent,
                children: [
                    {
                        path: '',
                        component: WebsiteInfoComponent
                    },
                    {
                        path: 'edit',
                        component: WebsiteInfoEditComponent
                    },
                    {
                        path: 'change-password',
                        component: ChangePasswordComponent
                    }
                ]
            },
            {
                path: 'order',
                data: { group: 'Orders', name: 'Orders' },
                component: OrdersComponent
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AccountRoutingModule { }