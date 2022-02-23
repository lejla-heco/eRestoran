import { Component } from '@angular/core';
import {NavigationStart, Router} from "@angular/router";
import {AutentifikacijaHelper} from "./helper/autentifikacija-helper";
import {MyConfig} from "./my-config";
import {LoginInformacije} from "./helper/login-informacije";
import {HttpClient} from "@angular/common/http";
import { Kupon } from './narudzba/view-models/kupon-vm';
import {SignalRService} from "./servisi/signalr.service";
import {AngularFireDatabase} from "@angular/fire/compat/database";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'eRestoranAngular';
  loginInformacije : LoginInformacije = new LoginInformacije();
  kuponi : Kupon[] = null;
  trenutnaSelekcija: string = "Home";
  closeModal : boolean = false;
  firebaseNotifikacija : boolean = false;
  firebaseSadrzajNotifikacije : string = "";

  constructor(public signalRService: SignalRService, private router: Router, private httpKlijent : HttpClient, private firebase : AngularFireDatabase) {
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
          this.loginInformacije = this.loginInfo();
        if (this.loginInformacije.isPermisijaKorisnik) this.preuzmiBrojKupona();
      }
    });
    router.navigateByUrl('home-page');
    signalRService.startConnection();
    signalRService.kuponNotifikacija();
    firebase.database.ref().child('PosebnaPonuda').on('value', (podatak)=>{
      if (podatak.val().Notifikacija && this.loginInformacije.isPermisijaKorisnik){
        this.firebaseSadrzajNotifikacije = "Snižena stavka menija: " + podatak.val().NazivStavke + " po cijeni: " + podatak.val().Cijena + " KM";
        this.firebaseNotifikacija = true;
        if (this.router.url == "/posebna-ponuda") {
          this.router.routeReuseStrategy.shouldReuseRoute = () => false;
          this.router.onSameUrlNavigation = 'reload';
          this.router.navigate(["/posebna-ponuda"]);
        }
      }
    });
  }

  odjava() {
    this.httpKlijent.post(MyConfig.adresaServera + "/Autentifikacija/Logout", null, MyConfig.httpOpcije())
      .subscribe((x: any) => {
      });
    AutentifikacijaHelper.ocistiMemoriju();
  }


  loginInfo() : LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  preuzmiKupone() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Kupon/GetAll", MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.kuponi = response;
    })
  }

  preuzmiBrojKupona(){
    this.httpKlijent.get(MyConfig.adresaServera + "/Kupon/GetBrojKupona", MyConfig.httpOpcije()).subscribe((response : any)=>{
      document.getElementById("notifikacije").innerHTML  = response;
    })
  }

  clicked(naziv : string){
    this.trenutnaSelekcija = naziv;
  }

  animirajNotifikaciju() {
    return this.closeModal == true? 'animate__animated animate__bounceOutUp' : 'animate__animated animate__bounceInDown';
  }

  zatvoriModal(){
    this.closeModal = true;
    this.animirajNotifikaciju();
    setTimeout(()=>{
      if (this.signalRService.podaci != null) this.signalRService.podaci = null;
      this.closeModal = false;
      if (this.firebaseNotifikacija) this.firebaseNotifikacija = false;
    },500);
  }

  preusmjeri() {
    if (this.trenutnaSelekcija!='Home') this.router.navigate(['/home-page']);
    setTimeout(()=>{
      this.router.navigate(['/home-page'], {fragment:'kontakt'});
    },500);
  }

  navigirajDoPosebnePonude() {
    this.router.navigate(['/posebna-ponuda']);
    this.zatvoriModal();
  }
}
