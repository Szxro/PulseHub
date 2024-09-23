import { HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable } from 'rxjs';
import { LocalStorageService } from '../services/local-storage.service';
import { TokenResponse } from '../models/responses/token-response';


export function tokenInterceptor(req:HttpRequest<unknown>,next: HttpHandlerFn):Observable<HttpEvent<unknown>>{
  const storageService = inject(LocalStorageService);
  
  const token = storageService.getItemByKey<TokenResponse>('token');
  
  if(token !== null && typeof token == 'object'){
    req = req.clone({
      setHeaders:{
        'Authorization':`Bearer ${token.token}`
      },
    });
  }
  return next(req);
}