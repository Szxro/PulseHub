import { Component, DestroyRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from '../../../../core/utils/custom-validators';
import { RegisterForm } from '../../models/register-form.model';
import { AuthService } from '../../services/auth-service.service';
import { RegisterRequest } from '../../models/register-request.model';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent{
  public registerForm: FormGroup<RegisterForm> = this.formBuilder.nonNullable.group({
    firstname: ['',[Validators.required, Validators.minLength(3)]],
    lastname:  ['', [Validators.required, Validators.minLength(3)]],
    username:  ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20), Validators.pattern("^[a-zA-Z0-9_]+$")]],
    email:     ['', [Validators.required, Validators.email]],
    password:  ['',[Validators.required, Validators.minLength(8), CustomValidators.isPasswordValid]]
  });
  
  constructor(
    private formBuilder: FormBuilder,
    private authService:AuthService,
    private destroyRef: DestroyRef
  ) {}


  onSubmit(){
    const request = this.registerForm.value as RegisterRequest; 
    
    this.authService.registerIn(request)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) =>{
        if(response !== undefined && response.status === 201){
          //TODO: Send a notification when the register was successfully and redirect to the login page
          console.log("Created succesfully,check your email...")
        }

        console.log("An error happen")
      });
  }
}
