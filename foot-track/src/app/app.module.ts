import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { AlertComponent } from './_components/alert/alert.component';
import { HomeComponent } from './home/home.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { fakeBackendProvider } from './_helpers/fake-backend.interceptor';
import { NavbarComponent } from './_components/navbar/navbar.component';
import { FooterComponent } from './_components/footer/footer.component';
import { FormTemplateComponent } from './_components/form-template/form-template.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    AlertComponent,
    HomeComponent,
    NavbarComponent,
    FooterComponent,
    FormTemplateComponent,
    ForgotPasswordComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

    //  provider used to create fake backend - remove it later
    fakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
