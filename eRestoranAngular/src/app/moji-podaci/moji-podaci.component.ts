import { Component, OnInit } from '@angular/core';
import {Korisnik} from "./view-models/korisnik-vm";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";
import {Opstina} from "../registracija/view-models/opstina-vm";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {LoginInformacije} from "../helper/login-informacije";

@Component({
  selector: 'app-moji-podaci',
  templateUrl: './moji-podaci.component.html',
  styleUrls: ['./moji-podaci.component.css']
})
export class MojiPodaciComponent implements OnInit {
  loginInformacije : LoginInformacije = null;
  korisnik : Korisnik = new Korisnik();
  opstine : Opstina[] = null;
  fieldText: boolean;
  constructor(private httpKlijent : HttpClient) {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    if (this.loginInformacije.isPermisijaKorisnik) this.getOpstine();
    this.ucitajPodatke();
  }

  private ucitajPodatke() {
    this.httpKlijent.get(MyConfig.adresaServera + "/KorisnickiNalog/Get", MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.korisnik = response;
    });
  }

  private getOpstine() {
    this.httpKlijent.get( MyConfig.adresaServera + "/Opstina/GetAll").subscribe((response : any)=>{
      this.opstine = response;
    });
  }

  prikaziSakrij() {
    this.fieldText = !this.fieldText;
  }

  generisiPreview() {
    // @ts-ignore
    var file = document.getElementById("fajl-input").files[0];

    if (file){
      var reader = new FileReader();
      reader.onload = function (){
        document.getElementById("preview-slika").setAttribute("src", reader.result.toString());
      }
      reader.readAsDataURL(file);
    }
  }

  podesiSirinuCarda() {
    return (this.loginInformacije.isPermisijaZaposlenik || this.loginInformacije.isPermisijaDostavljac) ? 'col-xl-12' :'col-xl-6';
  }
}
