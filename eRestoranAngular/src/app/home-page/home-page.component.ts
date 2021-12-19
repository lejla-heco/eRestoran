import { Component, OnInit } from '@angular/core';
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {PosebnaPonudaStavka} from "../posebna-ponuda/view-models/posebna-ponuda-stavka-vm";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {LoginInformacije} from "../helper/login-informacije";
import {Poslovnica} from "./view-models/poslovnica-vm";

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  public posebnaPonuda: PosebnaPonudaStavka[] = null;
  public poslovnice : Poslovnica[] = null;
  loginInformacije : LoginInformacije = null;
  mapsURL = "https://www.google.com/maps/d/embed?mid=1ZMProtfqXFwQwkAI_mscDbmRo7Bf70cy&ehbc=2E312F";

  constructor(private httpKlijent : HttpClient) {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
      if (AutentifikacijaHelper.getLoginInfo().isPermisijaKorisnik) {
        this.ucitajBrojStavki(this.loginInformacije.autentifikacijaToken.korisnickiNalog.id);
      }
  }

  ngOnInit(): void {
    this.getPosebnaPonuda();
    this.getPoslovnice();
  }

  private getPosebnaPonuda() {
    this.httpKlijent.get(MyConfig.adresaServera + "/PosebnaPonuda/GetAll").subscribe((result:any) =>{
      this.posebnaPonuda = result;
    })
  }

  private ucitajBrojStavki(id : number) {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/GetBrojStavki", MyConfig.httpOpcije()).subscribe((response : any) => {
      document.getElementById('kolicina').innerHTML = response;
    });
  }

  private getPoslovnice() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Poslovnica/GetAll").subscribe((response : any)=>{
      this.poslovnice = response;
    });
  }
}
