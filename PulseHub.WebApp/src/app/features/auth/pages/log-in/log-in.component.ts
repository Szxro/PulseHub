import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginForm } from '../../models/login-form.model';
import { LoginRequest } from '../../models/login-request.model';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.scss'
})
export class LogInComponent {
  public loginForm = new FormGroup<LoginForm>({
    username:new FormControl('',{ nonNullable:true,validators: [Validators.required,Validators.minLength(3),Validators.maxLength(20),Validators.pattern("^[a-zA-Z0-9_]+$")] }),
    password:new FormControl('',{ nonNullable:true })
  });

  constructor(private authService:AuthService){}

  onSubmit(){
    const request = this.loginForm.value as LoginRequest;

    this.authService.logIn(request).subscribe((response) =>{
      // TODO: Make type guard and show the user with an alert what happen
      if(!response.success && "reason" in response){

      }else{
    
      }
    });
  }
}
