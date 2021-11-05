import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { JwtModule } from '@auth0/angular-jwt';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { environment } from 'src/environments/environment';
import { API_URL } from './app-injection-tokens';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OrdersComponent } from './components/orders/orders.component';
import { DetailComponent } from './components//products/detail/detail.component';
import { ProductsComponent } from './components/products/products.component';
import { ACCESS_TOKEN_KEY } from './services/http/auth.http.service';
import { ModalWindowModule } from './module/modal-window.module';
import { CartComponent } from './components/cart/cart.component';
import { OrderLinesComponent } from './components/orders/order-lines/order-lines.component';
import { OrderCollapseComponent } from './components/orders/order-collapse/order-collapse.component';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY)
}

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    DetailComponent,
    OrdersComponent,
    CartComponent,
    OrderLinesComponent,
    OrderCollapseComponent
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ModalWindowModule,

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
