import { HttpErrorResponse, HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { LocalStorageService } from '../services/local-storage.service';
import { AuthService } from '../services/auth.service';
import { ToastService } from '../services/toast.service';
import { TokenResponse } from '../models/responses/token-response';


export function tokenInterceptor(req:HttpRequest<unknown>,next: HttpHandlerFn):Observable<HttpEvent<unknown>>{
  const storageService = inject(LocalStorageService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const toastService = inject(ToastService);
  
  const token = storageService.getItemByKey<TokenResponse>('token');
  
  if(token !== null && typeof token == 'object'){
    req = req.clone({
      setHeaders:{
        'Authorization':`Bearer ${token.token}`
      },
    });
  }
  return next(req).pipe(
    catchError((error) =>{
      if(error instanceof HttpErrorResponse && token !== null){
        switch(error.status){
          case 401:
            return authService.regenerateToken(token).pipe(
              switchMap((response) =>{
                storageService.setItemByKey('token',response.data);

                const newReq = req.clone({
                  setHeaders:{
                    'Authorization' : `Bearer ${response.data.token}`
                  }
                });

                return next(newReq);
              }),
              catchError((error) =>{
                storageService.purge();
                router.navigateByUrl('/auth/login');
                toastService.error({ message:'Session expired. Please log in again.' });

                return throwError(() => error);
              })
            )
          case 403:
            toastService.error({ message: 'Access forbidden. You do not have permission to access this resource.' });
            router.navigateByUrl('/home');
            break;
        }
      }
      return throwError(() => error);
    })
  );
}