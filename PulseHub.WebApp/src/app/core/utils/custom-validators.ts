import { AbstractControl, ValidationErrors } from "@angular/forms";

export class CustomValidators{
    static isPasswordValid(control:AbstractControl):ValidationErrors | null{
        const value = control.value || '';

        if(!/[A-Z]/.test(value)){
            return {invalidPassword:{ message: "The password at least must contain an uppercase" , value}}
        }

        if(!/[a-z]/.test(value)){
            return {invalidPassword:{ message: "The password at least must contain a lower case" , value}}
        }

        if(!/[0-9]/.test(value)){
            return {invalidPassword:{ message: "The password at least must contain a number" , value}}
        }

        if(!/[\W_]/.test(value)){
            return {invalidPassword:{ message: "The password at least must contain a special character" , value}}
        }

        return null;
    }
}