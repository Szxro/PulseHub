import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.scss'
})
export class LogInComponent {
  public loginForm = new FormGroup({
    username:new FormControl('',{ nonNullable:true,validators: [Validators.required,Validators.minLength(3),Validators.maxLength(20),Validators.pattern("^[a-zA-Z0-9_]+$")] }),
    password:new FormControl('',{ nonNullable:true, validators:[Validators.required]})
  });

  onSubmit(){
    if(!this.loginForm.valid){
      this.loginForm.markAllAsTouched();

      return;
    }
  }
}
