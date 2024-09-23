import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ErrorResponse } from '../models/responses/error-response.model';
import { isErrorResponse, isToastMessage } from '../utils/types-guards.util';
import { ToastMessage } from '../models/others/toast-message.model';

@Injectable({
    providedIn: 'root'
})
export class ToastService {
    constructor(
        private readonly toastr:ToastrService
    ) { }
    
    error(error:ToastMessage):void;
    error(error:ProgressEvent | ErrorResponse):void;
    error(error:unknown):void{
        switch(true){
            case error instanceof ProgressEvent:
                this.showError("Can't connect to the server, try again later",'Error');
                break;
            case isErrorResponse(error):
                this.handleErrorResponse(error);
                break;
            case isToastMessage(error):
                this.showError(error.message,error.title ?? 'Error');
                break;
            default:
                this.showError('An unexpected error occurred','Error');
                break;
        }
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