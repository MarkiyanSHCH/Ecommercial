import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { JwtModule } from '@auth0/angular-jwt';

import { environment } from 'src/environments/environment';
import { API_URL } from './app-injection-tokens';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OrdersComponent } from './components/orders/orders.component';
import { DetailComponent } from './components//products/detail/detail.component';
import { ProductsComponent } from './components/products/products.component';
import { ACCESS_TOKEN_KEY } from './services/auth.service';
import { AuthorizationDashboardModule } from './module/authorization-dashboard/authorization-dashboard.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY)
}

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    DetailComponent,
    OrdersComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AuthorizationDashboardModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.tokenWhiteListedDomins
      }
    }),
    NgbModule
  ],

  providers: [{
    provide: API_URL,
    useValue: environment.authApi
  }],

  bootstrap: [AppComponent]
})
export class AppModule { }
