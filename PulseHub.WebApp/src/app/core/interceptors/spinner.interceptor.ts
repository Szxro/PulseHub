import { HttpEvent, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize, Observable } from 'rxjs';
import { SpinnerService } from '../services/spinner.service';

// Functional interceptor
export function spinnerInterceptor(req:HttpRequest<unknown>,next:HttpHandlerFn):Observable<HttpEvent<unknown>>{
  const spinner = inject(SpinnerService);

  spinner.show();

  return next(req).pipe(
    finalize(() => spinner.hide())
  );
}
