import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { VALIDATION_ERROR_TOKEN } from '../../core/tokens/validation-error.token';
import { AUTH_VALIDATION_MESSAGES } from './constants/auth-validation-messages.constant';
import { SharedModule } from '../../shared/shared.module';


@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ],
  providers:[
    { provide:VALIDATION_ERROR_TOKEN, useValue:AUTH_VALIDATION_MESSAGES }
  ]
})
export class AuthModule { }
