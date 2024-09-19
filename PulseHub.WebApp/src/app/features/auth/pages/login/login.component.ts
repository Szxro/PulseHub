import { Component, DestroyRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, EMPTY } from 'rxjs';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../core/services/toast.service';
import { ErrorResponse } from '../../../../core/models/responses/error-response.model';
import { LoginRequest } from '../../../../core/models/requests/auth-request.model';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  public readonly loginForm = new FormGroup({
    username:new FormControl('',[Validators.required,Validators.minLength(3)]),
    password:new FormControl('',[Validators.required])
  });

  constructor(
    private readonly _authService:AuthService,
    private readonly _router:Router,
    private readonly _toast:ToastService,
    private readonly _destroyRef:DestroyRef
  ){}
  
  public onSubmit(){
    this._authService.login(
      this.loginForm.value as LoginRequest
    )
    .pipe(
      takeUntilDestroyed(this._destroyRef),
      catchError((err: ProgressEvent | ErrorResponse) =>{
        this._toast.error(err);
        // Emit no values just complete the observable (complete: => complete signal)
        return EMPTY; 
      })
    )
    .subscribe(_ =>{
      this._router.navigate(['/home']);
    });
    
    this.loginForm.reset();
  }
}
