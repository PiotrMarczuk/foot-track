import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { fakeBackendProvider } from './helpers/fake-backend.interceptor';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

    //  provider used to create fake backend - remove it later
    fakeBackendProvider
  ]
})
export class CoreModule { }
