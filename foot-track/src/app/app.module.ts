import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginPageComponent } from './login/login-page/login-page.component';
import { LoginFormComponent } from './login/login-form/login-form.component';
import { FormsModule } from '@angular/forms';
import { AlertComponent } from './_components/alert/alert.component';
import { HomeComponent } from './home/home.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { fakeBackendProvider } from './_helpers/fake-backend.interceptor';
import { RegisterFormComponent } from './register/register-form/register-form.component';
import { RegisterPageComponent } from './register/register-page/register-page.component';
import { ForgotPasswordPageComponent } from './forgot-password/forgot-password-page/forgot-password-page.component';
import { ForgotPasswordFormComponent } from './forgot-password/forgot-password-form/forgot-password-form.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    LoginFormComponent,
    AlertComponent,
    HomeComponent,
    RegisterPageComponent,
    RegisterFormComponent,
    ForgotPasswordPageComponent,
    ForgotPasswordFormComponent
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
