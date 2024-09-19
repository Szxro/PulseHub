import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ErrorResponse } from '../models/responses/error-response.model';
import { isErrorResponse } from '../utils/types-guards.util';
import { ToastMessage } from '../models/others/toast-message.model';

@Injectable({
    providedIn: 'root'
})
export class ToastService {
    constructor(
        private readonly toastr:ToastrService
    ) { }

    error(error:ProgressEvent | ErrorResponse):void;
    error(error:unknown):void{
        if(error instanceof ProgressEvent){
            this.showError("Can't connect to the server, try again later",'Error');
            return;
        }

        if(isErrorResponse(error)){
            this.handleErrorResponse(error);
            return;
        }

        this.showError("An unexpected error occurred", 'Error');
    }

    sucess({message,title = "Success!!"}:ToastMessage){
        this.toastr.success(message,title,{
            progressBar:true,
            closeButton:true,
            progressAnimation:'decreasing'
        })
    }
    
    private handleErrorResponse(error:ErrorResponse):void{
        if(error.errors !== undefined){
            for(const errors of Object.values(error.errors)){
                for(const { message } of errors){
                    this.showError(message,'Error');
                }
            }
            return;
        }
        this.showError(error.detail,"Error");
    }
    
    private showError(message:string,title:string){
        this.toastr.error(message,title,{
            progressBar:true,
            closeButton:true,
            progressAnimation:'decreasing'
        });
    }
}