import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../../../core/models/login-request.model';
import { catchError, Observable, of } from 'rxjs';
import { TokenResponse } from '../../../core/models/token-response.model';
import { RegisterRequest } from '../../../core/models/register-request.model';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService{
  constructor(private http:HttpClient) { }

  logIn(request:LoginRequest):Observable<TokenResponse | undefined>{
    return this.http.post<TokenResponse>(`${environment.baseUrl}/api/user/login-user`,request).pipe(catchError(_ => of(undefined)));
  }

  // The observe option let you gain access to HTTP response object
  registerIn(request:RegisterRequest):Observable<HttpResponse<void> | undefined>{
    return this.http.post<void>(`${environment.baseUrl}/api/user/create-user`,request,{ observe:'response' }).pipe(catchError(_ => of(undefined)));
  }
}
