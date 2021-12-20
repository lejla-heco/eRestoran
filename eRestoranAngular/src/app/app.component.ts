import { Component } from '@angular/core';
import {NavigationStart, Router} from "@angular/router";
import {AutentifikacijaHelper} from "./helper/autentifikacija-helper";
import {MyConfig} from "./my-config";
import {LoginInformacije} from "./helper/login-informacije";
import {HttpClient} from "@angular/common/http";
import { Kupon } from './narudzba/view-models/kupon-vm';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'eRestoranAngular';
  loginInformacije : LoginInformacije = new LoginInformacije();
  kuponi : Kupon[] = null;

  constructor(private router: Router, private httpKlijent : HttpClient) {
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
          this.loginInformacije = this.loginInfo();
      }
    });
    router.navigateByUrl('home-page');
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
}
