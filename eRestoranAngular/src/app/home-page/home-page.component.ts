import { Component, OnInit } from '@angular/core';
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {PosebnaPonudaStavka} from "../posebna-ponuda/view-models/posebna-ponuda-stavka-vm";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {LoginInformacije} from "../helper/login-informacije";

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  public posebnaPonuda: PosebnaPonudaStavka[] = null;
  loginInformacije : LoginInformacije = null;

  constructor(private httpKlijent : HttpClient) {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
      if (AutentifikacijaHelper.getLoginInfo().isPermisijaKorisnik) {
        this.ucitajBrojStavki(this.loginInformacije.autentifikacijaToken.korisnickiNalog.id);
      }
  }

  ngOnInit(): void {
    this.getPosebnaPonuda();

  }

  private getPosebnaPonuda() {
    this.httpKlijent.get(MyConfig.adresaServera + "/PosebnaPonuda/GetAll").subscribe((result:any) =>{
      this.posebnaPonuda = result;
    })
  }

  private ucitajBrojStavki(id : number) {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/GetBrojStavki/" + id).subscribe((response : any) => {
      document.getElementById('kolicina').innerHTML = response;
    });
  }
}
