import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { JwtModule } from '@auth0/angular-jwt';

import { environment } from 'src/environments/environment';
import { AUTH_API_URL, STORE_API_URL } from './app-injection-tokens';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OrdersComponent } from './components/orders/orders.component';
import { DetailComponent } from './product/detail/detail.component';
import { ProductComponent } from './product/product.component';
import { ACCESS_TOKEN_KEY } from './services/auth.service';

export function tokenGetter(){
  return localStorage.getItem(ACCESS_TOKEN_KEY)
}

@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    DetailComponent,
    OrdersComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    
    JwtModule.forRoot({
      config:{
        tokenGetter,
        allowedDomains: environment.tokenWhiteListedDomins
      }
    })
  ],

  providers: [{
    provide: AUTH_API_URL,
    useValue: environment.authApi
  },
  {
    provide: STORE_API_URL,
    useValue: environment.storeApi
  }],

  bootstrap: [AppComponent]
})
export class AppModule { }
