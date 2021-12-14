import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from "@angular/common/http";
import {RouterModule} from "@angular/router";
import {FormsModule} from "@angular/forms";
import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { PosebnaPonudaComponent } from './posebna-ponuda/posebna-ponuda.component';
import { MeniComponent } from './meni/meni.component';
import { NovaStavkaComponent } from './meni/nova-stavka/nova-stavka.component';
import { EditStavkaComponent } from './meni/edit-stavka/edit-stavka.component';
import { LoginComponent } from './login/login.component';
import { NarudzbaComponent } from './narudzba/narudzba.component';
import { StarComponent } from './meni/star/star.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import { KuponComponent } from './kupon/kupon.component';
import { ZaposlenikComponent } from './zaposlenik/zaposlenik.component';
import { NoviZaposlenikComponent } from './zaposlenik/novi-zaposlenik/novi-zaposlenik.component';
import { EditZaposlenikComponent } from './zaposlenik/edit-zaposlenik/edit-zaposlenik.component';
import { OmiljenjeStavkeComponent } from './omiljenje-stavke/omiljenje-stavke.component';
import { PoslovnicaComponent } from './poslovnica/poslovnica.component';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    PosebnaPonudaComponent,
    MeniComponent,
    NovaStavkaComponent,
    EditStavkaComponent,
    LoginComponent,
    NarudzbaComponent,
    StarComponent,
    RegistracijaComponent,
    KuponComponent,
    ZaposlenikComponent,
    NoviZaposlenikComponent,
    EditZaposlenikComponent,
    OmiljenjeStavkeComponent,
    PoslovnicaComponent

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path:"home-page", component:HomePageComponent},
      {path:"posebna-ponuda", component:PosebnaPonudaComponent},
      {path:"meni", component:MeniComponent},
      {path:"nova-stavka", component:NovaStavkaComponent},
      {path:"edit-stavka/:id", component:EditStavkaComponent},
      {path:"login", component:LoginComponent},
      {path:"narudzba", component:NarudzbaComponent},
      {path:"registracija", component:RegistracijaComponent},
      {path:"kupon", component:KuponComponent},
      {path:"zaposlenik", component:ZaposlenikComponent},
      {path:"novi-zaposlenik", component:NoviZaposlenikComponent},
      {path:"edit-zaposlenik/:id", component:EditZaposlenikComponent},
      {path:"omiljene-stavke", component:OmiljenjeStavkeComponent},
      {path:"poslovnica", component:PoslovnicaComponent}

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
