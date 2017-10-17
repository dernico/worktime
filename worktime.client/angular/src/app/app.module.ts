import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpInterceptorService } from './services/httpInterceptor.service';

import { AppComponent } from './components/app/app.component';
import { TasksComponent } from './components/tasks/tasks.component';
import { StartComponent } from './components/start/start.component';
import { NavigationComponent } from './components/navigation/navigation.component';

import { LoginService } from "./services/login.service";

@NgModule({
  declarations: [
    AppComponent,
    TasksComponent,
    StartComponent,
    NavigationComponent
  ],
  imports: [
    RouterModule.forRoot([
      {
        path: '',
        component: StartComponent
      },
      {
        path: 'tasks',
        component: TasksComponent
      }
    ], { useHash: true }),
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    LoginService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
