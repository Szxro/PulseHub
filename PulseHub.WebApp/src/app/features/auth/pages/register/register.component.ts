import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../core/services/toast.service';
import { CustomValidators } from '../../../../shared/validators/custom-validators';
import { ErrorResponse } from '../../../../core/models/responses/error-response.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnDestroy {
  private readonly _onDestroy$ = new Subject<void>();

  public readonly registerForm = this._formBuilder.nonNullable.group({
    firstname:['',[Validators.required]],
    lastname:['',[Validators.required]],
    username:['',[Validators.required,CustomValidators.usernameCheck]],
    email:['',[Validators.required]],
    password:['',[Validators.required,CustomValidators.passwordStrengthCheck,Validators.minLength(6)]],
    confirmPassword:['',[Validators.required]]
  },{
    // List of validators applied to the form group (global validators)
    validators: [ CustomValidators.passwordMatch ] 
  });

  constructor(
    private readonly _formBuilder:FormBuilder,
    private readonly _authService:AuthService,
    private readonly _router:Router,
    private readonly _toast:ToastService
  ){}


  ngOnDestroy(): void {
    this._onDestroy$.next();
    this._onDestroy$.complete();
  }

  public onSubmit(){
    const { confirmPassword, ...credentials } = this.registerForm.getRawValue();

    this._authService.register(credentials)
        .pipe(
          takeUntil(this._onDestroy$),
          catchError((err:ProgressEvent | ErrorResponse) =>{
            this._toast.error(err);

            return EMPTY;
          })
        )
        .subscribe(_ =>{
          this._toast.sucess({ message:"You successfully created an account on PulseHub!!" });
          this._router.navigateByUrl('/auth/login');
        });

        this.registerForm.reset();
    }
}
