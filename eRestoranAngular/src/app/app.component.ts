import { Component } from '@angular/core';
import {NavigationStart, Router} from "@angular/router";
import {AutentifikacijaHelper} from "./helper/autentifikacija-helper";
import {MyConfig} from "./my-config";
import {LoginInformacije} from "./helper/login-informacije";
import {HttpClient} from "@angular/common/http";
import { Kupon } from './narudzba/view-models/kupon-vm';
import {SignalRService} from "./servisi/signalr.service";

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

  constructor(public signalRService: SignalRService, private router: Router, private httpKlijent : HttpClient) {
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
          this.loginInformacije = this.loginInfo();
      }
    });
    router.navigateByUrl('home-page');
    if (this.loginInformacije.isPermisijaKorisnik) this.preuzmiBrojKupona();
    signalRService.startConnection();
    signalRService.kuponNotifikacija();
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
}
