import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { TasksComponent } from './components/tasks/tasks.component';
import { StartComponent } from './components/start/start.component';
import { NavigationComponent } from './components/navigation/navigation.component';

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
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
