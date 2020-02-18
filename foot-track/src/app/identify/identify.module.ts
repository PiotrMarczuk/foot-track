import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../core/core.module';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FormControl, FormsModule } from '@angular/forms';
import { FormTemplateComponent } from './components/form-template/form-template.component';
import { AlertComponent } from '../core/components/alert/alert.component';
import { IdentifyRoutingModule } from './identify-routing.module';



@NgModule({
  declarations: [
    ForgotPasswordComponent,
    LoginComponent,
    RegisterComponent,
    FormTemplateComponent,
    AlertComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    FormsModule,
    IdentifyRoutingModule
  ]
})
export class IdentifyModule { }
