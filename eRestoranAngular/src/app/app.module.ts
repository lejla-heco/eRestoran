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
import { AutorizacijaAdminProvjera } from "./guards/autorizacija-admin-provjera.service";
import { AutorizacijaKorisnikProvjera } from "./guards/autorizacija-korisnik-provjera.service";
import { GoogleMapsModule } from '@angular/google-maps';
import { PoslovnicaComponent } from './poslovnica/poslovnica.component';
import { DostavljacComponent } from './dostavljac/dostavljac.component';
import { NoviDostavljacComponent } from './dostavljac/novi-dostavljac/novi-dostavljac.component';
import { EditDostavljacComponent } from './dostavljac/edit-dostavljac/edit-dostavljac.component';
import { RezervacijaComponent } from './rezervacija/rezervacija.component';
import { PregledRezervacijaComponent } from './rezervacija/pregled-rezervacija/pregled-rezervacija.component';
import { OmiljeneNarudzbeComponent } from './omiljene-narudzbe/omiljene-narudzbe.component';
import { MojeNarudzbeComponent } from './moje-narudzbe/moje-narudzbe.component';
import { PregledNarudzbiComponent } from './pregled-narudzbi/pregled-narudzbi.component';
import {AutorizacijaZaposlenikProvjera} from "./guards/autorizacija-zaposlenik-provjera.service";
import { PregledNarudzbiDostavljacComponent } from './pregled-narudzbi-dostavljac/pregled-narudzbi-dostavljac.component';
import {AutorizacijaDostavljacProvjera} from "./guards/autorizacija-dostavljac-provjera.service";
import { PregledRezervacijaZaposlenikComponent } from './pregled-rezervacija-zaposlenik/pregled-rezervacija-zaposlenik.component';
import { MojiPodaciComponent } from './moji-podaci/moji-podaci.component';
import { EditPoslovnicaComponent } from './poslovnica/edit-poslovnica/edit-poslovnica.component';


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
    PoslovnicaComponent,
    DostavljacComponent,
    NoviDostavljacComponent,
    EditDostavljacComponent,
    RezervacijaComponent,
    PregledRezervacijaComponent,
    OmiljeneNarudzbeComponent,
    MojeNarudzbeComponent,
    PregledNarudzbiComponent,
    PregledNarudzbiDostavljacComponent,
    PregledRezervacijaZaposlenikComponent,
    MojiPodaciComponent,
    EditPoslovnicaComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path:"home-page", component:HomePageComponent},
      {path:"posebna-ponuda", component:PosebnaPonudaComponent},
      {path:"meni", component:MeniComponent},
      {path:"nova-stavka", component:NovaStavkaComponent, canActivate:[AutorizacijaAdminProvjera]},
      {path:"edit-stavka/:id", component:EditStavkaComponent, canActivate:[AutorizacijaAdminProvjera]},
      {path:"narudzba", component:NarudzbaComponent, canActivate:[AutorizacijaKorisnikProvjera]},
      {path:"kupon", component:KuponComponent, canActivate:[AutorizacijaAdminProvjera]},
      {path:"zaposlenik", component:ZaposlenikComponent, canActivate:[AutorizacijaAdminProvjera]},
      {path:"novi-zaposlenik", component:NoviZaposlenikComponent, canActivate:[AutorizacijaAdminProvjera]},
      {path:"edit-zaposlenik/:id", component:EditZaposlenikComponent, canActivate:[AutorizacijaAdminProvjera]},
      {path:"omiljene-stavke", component:OmiljenjeStavkeComponent, canActivate:[AutorizacijaKorisnikProvjera]},
      {path:"poslovnica", component:PoslovnicaComponent, canActivate:[AutorizacijaAdminProvjera]},
      {path:"edit-poslovnica/:id", component:EditPoslovnicaComponent, canActivate:[AutorizacijaAdminProvjera]},
      {path:"login", component:LoginComponent},
      {path:"registracija", component:RegistracijaComponent},
      {path:"dostavljac",component:DostavljacComponent,canActivate:[AutorizacijaAdminProvjera]},
      {path:"novi-dostavljac", component:NoviDostavljacComponent, canActivate:[AutorizacijaAdminProvjera]},
      {path:"edit-dostavljac/:id", component:EditDostavljacComponent, canActivate:[AutorizacijaAdminProvjera]},
      {path:"rezervacija", component:RezervacijaComponent, canActivate:[AutorizacijaKorisnikProvjera]},
      {path:"pregled-rezervacija", component:PregledRezervacijaComponent, canActivate:[AutorizacijaKorisnikProvjera]},
      {path:"omiljene-narudzbe", component:OmiljeneNarudzbeComponent, canActivate:[AutorizacijaKorisnikProvjera]},
      {path:"moje-narudzbe", component:MojeNarudzbeComponent, canActivate:[AutorizacijaKorisnikProvjera]},
      {path:"pregled-narudzbi", component:PregledNarudzbiComponent, canActivate:[AutorizacijaZaposlenikProvjera]},
      {path:"pregled-narudzbi-dostavljac", component:PregledNarudzbiDostavljacComponent, canActivate:[AutorizacijaDostavljacProvjera]},
      {path:"pregled-rezervacija-zaposlenik",component:PregledRezervacijaZaposlenikComponent,canActivate:[AutorizacijaZaposlenikProvjera]},
      {path:"moji-podaci", component:MojiPodaciComponent, canActivate:[AutorizacijaKorisnikProvjera]}
    ],{
      anchorScrolling : 'enabled',
      scrollPositionRestoration : 'enabled',
      onSameUrlNavigation: 'reload'
    }),
    GoogleMapsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
