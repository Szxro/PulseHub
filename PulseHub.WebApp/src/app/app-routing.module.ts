import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';

const routes: Routes = [
  {
    path:'',
    pathMatch:'full',
    redirectTo:'/auth/login'
  },
  {
    path:'home',
    loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule),
    // TODO: GUARD THE HOME ROUTE FOR UNAUTHORIZED USERS
  },
  {
    path:'auth',
    loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path:'**',
    component:NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
