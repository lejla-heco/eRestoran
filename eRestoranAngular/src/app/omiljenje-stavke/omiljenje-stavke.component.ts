import { Component, OnInit } from '@angular/core';
import {Uloga} from "../helper/uloga";
import {OmiljenaStavka} from "./view-models/omiljena-stavka-vm";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";
import {MeniGrupa} from "../meni/view-models/meni-grupa-vm";
import {StavkaNarudzbe} from "../narudzba/view-models/stavka-narudzbe-vm";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {LoginInformacije} from "../helper/login-informacije";

@Component({
  selector: 'app-omiljenje-stavke',
  templateUrl: './omiljenje-stavke.component.html',
  styleUrls: ['./omiljenje-stavke.component.css']
})
export class OmiljenjeStavkeComponent implements OnInit {
  omiljeneStavke: OmiljenaStavka[] = null;
  public meniGrupe: MeniGrupa[] = null;
  private novaStavkaNarudzbe : StavkaNarudzbe = new StavkaNarudzbe();
  loginInformacije : LoginInformacije = null;

  constructor(private httpKlijent : HttpClient) {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.getMeniGrupe();
    this.ucitajOmiljeneStavke();
  }

  ucitajOmiljeneStavke(kategorija : string = "Doručak") {
    var podaci : any = new Object();
    podaci.id = this.loginInformacije.autentifikacijaToken.korisnickiNalog.id;
    podaci.kategorija = kategorija;
    this.httpKlijent.post(MyConfig.adresaServera + '/OmiljenaStavka/GetAllPaged/', podaci).subscribe((response : any)=> {
      this.omiljeneStavke = response;
    })
  }

  createRange(ocjena: number) {
    let velicina = Math.round(ocjena);
    return new Array(velicina);
  }

  private getMeniGrupe() {
    this.httpKlijent.get(MyConfig.adresaServera + "/MeniGrupa/GetAll").subscribe((result : any)=>{
      this.meniGrupe = result;
    })
  }

  dodajUKorpu(stavka : OmiljenaStavka) {
    this.novaStavkaNarudzbe.korisnikId = this.loginInformacije.autentifikacijaToken.korisnickiNalog.id;
    this.novaStavkaNarudzbe.meniStavkaId = stavka.id;
    this.httpKlijent.post(MyConfig.adresaServera+"/Narudzba/AddStavka",this.novaStavkaNarudzbe).subscribe((response : any)=>{
      document.getElementById('kolicina').innerHTML = response;
    });
  }

  obrisiStavku(stavka : OmiljenaStavka) {
    this.httpKlijent.get(MyConfig.adresaServera+"/OmiljenaStavka/Delete/" + stavka.omiljenaStavkaId).subscribe((response : any)=>{
      alert("Uspjesno uklonjena stavka");
      this.ucitajOmiljeneStavke(stavka.nazivGrupe);
    });
  }
}
