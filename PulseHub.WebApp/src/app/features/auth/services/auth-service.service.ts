import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { TokenResponse } from '../../../core/models/token-response.model';
import { RegisterRequest } from '../models/register-request.model';
import { environment } from '../../../../environments/environment.development';
import { LocalStorageService } from '../../../core/services/local-storage.service';
import { SuccessResponse } from '../../../core/models/success-response.model';
import { Outcome } from '../../../core/models/outcome.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService{
  constructor(
    private http:HttpClient,
    private storageService:LocalStorageService
  ) { }

  logIn(request:LoginRequest):Observable<Outcome<TokenResponse>>{
    return this.http.post<SuccessResponse<TokenResponse>>(`${environment.baseUrl}/api/user/login-user`,request)
          .pipe(
            tap({ next:(response) => this.storageService.setItemByKey('authToken',response.data) }),
            map(response => ({ success: true, data: response })),
            catchError(error => {
              if(error instanceof HttpErrorResponse){
                return of({success: false, reason: error.error})
              }

              return of({success: false, reason: error});
            })
          );
  }

  // The observe option let you gain access to HTTP response object
  registerIn(request:RegisterRequest):Observable<HttpResponse<void> | undefined>{
    return this.http.post<void>(`${environment.baseUrl}/api/user/create-user`,request,{ observe:'response' })
             .pipe(
              catchError(_ => of(undefined))
            );
  }
}