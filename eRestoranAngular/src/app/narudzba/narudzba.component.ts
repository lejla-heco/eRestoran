import { Component, OnInit } from '@angular/core';
import {Narudzba} from "./view-models/narudzba-vm";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";
import {StavkaNarudzbe} from "./view-models/stavka-narudzbe-vm";
import {NarudzbaStavka} from "./view-models/narudzba-stavka";
import {NovaKolicina} from "./view-models/update-kolicina-vm";

@Component({
  selector: 'app-narudzba',
  templateUrl: './narudzba.component.html',
  styleUrls: ['./narudzba.component.css']
})
export class NarudzbaComponent implements OnInit {
  narudzba : Narudzba = null;
  id : number;
  podaci : StavkaNarudzbe = new StavkaNarudzbe();
  updateKolicina : NovaKolicina = new NovaKolicina();

  constructor(private httpKlijent : HttpClient) {
    if (sessionStorage.getItem("autentifikacija-token") || localStorage.getItem("autentifikacija-token")) {
      var korisnik = JSON.parse(sessionStorage.getItem("autentifikacija-token"));
      if (korisnik == null) korisnik = JSON.parse(localStorage.getItem("autentifikacija-token"));
      this.id = korisnik.korisnickiNalog.id;
    }
  }

  ngOnInit(): void {
    this.ucitajNarudzbu();
  }

  private ucitajNarudzbu() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/GetNarudzba/" + this.id).subscribe((response : any)=>{
      this.narudzba = response;
    });
  }

  ukloni(stavka : NarudzbaStavka) {
    this.httpKlijent.get(MyConfig.adresaServera+"/Narudzba/UkloniStavku/" + stavka.id).subscribe((response : any)=>{
      this.narudzba = response;
      this.ucitajBrojStavki(this.id);
    });
  }

  private ucitajBrojStavki(id : number) {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/GetBrojStavki/" + id).subscribe((response : any) => {
      document.getElementById('kolicina').innerHTML = response;
    });
  }

  public novaKolicina(stavka : NarudzbaStavka){
     this.updateKolicina.id = stavka.id;
     this.updateKolicina.kolicina = stavka.kolicina;
    this.httpKlijent.post(MyConfig.adresaServera + "/Narudzba/UpdateKolicina", this.updateKolicina).subscribe((response : any)=>{
      this.narudzba.cijena = response.cijena;
      document.getElementById('kolicina').innerHTML = response.kolicina;
    });
  }
}
