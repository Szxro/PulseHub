import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { SharedModule } from './shared/shared.module';
import { spinnerInterceptor } from './core/interceptors/spinner.interceptor';
import { tokenInterceptor } from './core/interceptors/token.interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({ 
      timeOut:3000,
      preventDuplicates: true 
    }),
    SharedModule
  ],
  providers: [
    provideHttpClient(
      withInterceptors([ spinnerInterceptor, tokenInterceptor ])
    )
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
