import { Injectable,Inject } from '@angular/core';
import { LocalStorageService } from './local-storage.service';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { LoginRequest, RegisterRequest } from '../models/requests/auth-request.model';
import { TokenResponse } from "../models/responses/token-response"
import { SuccessResponse } from '../models/responses/success-response.model';
import { API_URL_TOKEN } from '../tokens/api-url.token';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(
        private readonly _storageService:LocalStorageService,
        private readonly _http:HttpClient,
        @Inject(API_URL_TOKEN) private readonly _apiUrl:string,
    ) { }

    login(credentials:LoginRequest):Observable<SuccessResponse<TokenResponse>>{
        return this._http.post<SuccessResponse<TokenResponse>>(
            `${this._apiUrl}/user/login-user`,
            credentials
        ).pipe(
            tap({
                next: (response) => this._storageService.setItemByKey('token',response.data)
            }),
            catchError((err:HttpErrorResponse) => throwError(() => err.error))
        );
    };

    register(credentials:RegisterRequest):Observable<HttpResponse<Object>>{
        return this._http.post<HttpResponse<Object>>(
            `${this._apiUrl}/user/create-user`
            ,credentials,
            { observe:'response' }
        ).pipe(
            catchError((err:HttpErrorResponse) => throwError(() => err.error) )
        );
    }

    logout():void{
        this._storageService.removeItemByKey('token');
    }

    isAuthenticated():boolean{
        const token = this._storageService.getItemByKey<TokenResponse>('token');
        return !!token;
    }
}