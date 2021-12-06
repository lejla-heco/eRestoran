import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from "@angular/common/http";
import {RouterModule} from "@angular/router";

import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { PosebnaPonudaComponent } from './posebna-ponuda/posebna-ponuda.component';
import { MeniComponent } from './meni/meni.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    PosebnaPonudaComponent,
    MeniComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      {path:"home-page", component:HomePageComponent},
      {path:"posebna-ponuda", component:PosebnaPonudaComponent},
      {path:"meni", component:MeniComponent}
    ],{
      anchorScrolling : 'enabled',
      scrollPositionRestoration : 'enabled',
      onSameUrlNavigation: 'reload'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
