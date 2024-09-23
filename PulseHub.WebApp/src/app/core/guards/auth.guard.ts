import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

export function AuthGuard(route: ActivatedRouteSnapshot,state: RouterStateSnapshot){
    const router = inject(Router);
    const authService = inject(AuthService);

    const isAuthenticated = authService.isAuthenticated();
    
    if(!isAuthenticated){
        router.navigateByUrl('/auth/login');

        return false;
    }

    return true;
}