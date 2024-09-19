import { AfterViewInit, Directive, ElementRef, Inject, Input, OnDestroy } from '@angular/core';
import { AbstractControl, FormGroup, ValidationErrors } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { VALIDATION_ERROR_TOKEN } from '../../core/tokens/validation-error.token';
import { ErrorHandler } from '../../core/models/handlers/error-handler.model';

@Directive({
     selector: '[errorMessage]'
 })
export class ErrorMessageDirective implements AfterViewInit,OnDestroy {
    @Input({ required: true }) target!: FormGroup | AbstractControl;

    private destroy$ = new Subject<void>();

    constructor(
        private _element:ElementRef<HTMLSpanElement>,
        @Inject(VALIDATION_ERROR_TOKEN) private _errors: ErrorHandler
    ) { }
    
    ngAfterViewInit(): void {
        if(this.target instanceof FormGroup){
            this.target.statusChanges.pipe(
                takeUntil(this.destroy$)
            ).subscribe(() => this.setGlobalErrorMessages());
        }else{
            this.target.statusChanges.pipe(
                takeUntil(this.destroy$)
            ).subscribe(() => this.setFieldGlobalErrorMessages());
        }

    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }
    
    private getErrorMessage(field:"field" | "global",errors:ValidationErrors):string{
        const [errorKey, errorValue] = Object.entries(errors)[0];

        const message = this._errors[field]?.[errorKey] 
        ? this._errors[field][errorKey](errorValue) 
        : "Unknown message";

        return message;
    }
    
    private setGlobalErrorMessages(){
        if(this.target.errors){
            const message = this.getErrorMessage('global',this.target.errors);

            this._element.nativeElement.textContent = message;
        }else{
            this._element.nativeElement.textContent = '';
        }
    }
    
    private setFieldGlobalErrorMessages(){
        if(this.target.invalid && this.target.dirty && this.target.errors){
            const message = this.getErrorMessage('field',this.target.errors);

            this._element.nativeElement.textContent = message;
        }else{
            this._element.nativeElement.textContent = '';
        }
    }

}