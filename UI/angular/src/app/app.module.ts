import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { JwtModule } from '@auth0/angular-jwt';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { environment } from 'src/environments/environment';
import { API_URL } from './app-injection-tokens';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ACCESS_TOKEN_KEY } from './services/http/auth.http.service';
import { AnonymousGuard } from './guards/anonymous.guard';
import { CoreModule } from './core/core.module';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY)
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CoreModule,
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
  },
    AnonymousGuard
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
