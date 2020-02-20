import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../core/core.module';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormTemplateComponent } from './components/form-template/form-template.component';
import { AlertComponent } from '../core/components/alert/alert.component';
import { IdentifyRoutingModule } from './identify-routing.module';
import { MustMatchDirective } from './directives/must-match.directive';



@NgModule({
  declarations: [
    ForgotPasswordComponent,
    LoginComponent,
    RegisterComponent,
    FormTemplateComponent,
    AlertComponent,
    MustMatchDirective
  ],
  imports: [
    CommonModule,
    CoreModule,
    FormsModule,
    IdentifyRoutingModule,
    ReactiveFormsModule
  ],
  exports: [MustMatchDirective]
})
export class IdentifyModule { }
