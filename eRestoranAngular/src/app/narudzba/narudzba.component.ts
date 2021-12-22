import { Component, OnInit } from '@angular/core';
import {Narudzba} from "./view-models/narudzba-vm";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";
import {StavkaNarudzbe} from "./view-models/stavka-narudzbe-vm";
import {NarudzbaStavka} from "./view-models/narudzba-stavka";
import {NovaKolicina} from "./view-models/update-kolicina-vm";
import {Kupon} from "./view-models/kupon-vm";

@Component({
  selector: 'app-narudzba',
  templateUrl: './narudzba.component.html',
  styleUrls: ['./narudzba.component.css']
})
export class NarudzbaComponent implements OnInit {
  narudzba : Narudzba = null;
  podaci : StavkaNarudzbe = new StavkaNarudzbe();
  updateKolicina : NovaKolicina = new NovaKolicina();
  kuponi : Kupon[] = null;
  odabraniKupon : Kupon = new Kupon();
  zakljuciNarudzbu : boolean = false;

  constructor(private httpKlijent : HttpClient) {
  }

  ngOnInit(): void {
    this.ucitajNarudzbu();
    this.ucitajKupone();
  }

  private ucitajNarudzbu() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/GetNarudzba", MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.narudzba = response;
    });
  }

  ukloni(stavka : NarudzbaStavka) {
    this.httpKlijent.get(MyConfig.adresaServera+"/Narudzba/UkloniStavku/" + stavka.id, MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.narudzba = response;
      this.ucitajBrojStavki();
    });
  }

  private ucitajBrojStavki() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/GetBrojStavki", MyConfig.httpOpcije()).subscribe((response : any) => {
      document.getElementById('kolicina').innerHTML = response;
    });
  }

  public novaKolicina(stavka : NarudzbaStavka){
     this.updateKolicina.id = stavka.id;
     this.updateKolicina.kolicina = stavka.kolicina;
    this.httpKlijent.post(MyConfig.adresaServera + "/Narudzba/UpdateKolicina", this.updateKolicina, MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.narudzba.cijena = response.cijena;
      document.getElementById('kolicina').innerHTML = response.kolicina;
    });
  }

  upravljajOmiljenomNarudzbom(narudzba: Narudzba) {
    if (narudzba.omiljeno){
      narudzba.omiljeno = false;
      this.ukloniOmiljeno(narudzba);
    }
    else {
      narudzba.omiljeno = true;
      this.dodajOmiljeno(narudzba);
    }
  }

  private ukloniOmiljeno(narudzba: Narudzba) {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/UkloniOmiljenu/" + narudzba.id, MyConfig.httpOpcije()).subscribe((response : any)=>{

    });
  }

  private dodajOmiljeno(narudzba: Narudzba){
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/OmiljenaNarudzba/" + narudzba.id, MyConfig.httpOpcije()).subscribe((response : any)=>{

    });
  }

  private ucitajKupone() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Kupon/GetAll", MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.kuponi = response;
    })
  }

  zatvoriModal() {
    this.zakljuciNarudzbu = false;
    this.odabraniKupon.id = 0;
  }

  posaljiKupon() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Kupon/PrimijeniKupon/" + this.odabraniKupon.id, MyConfig.httpOpcije()).subscribe((response : any)=>{
      document.getElementById("cijenaPopust").innerHTML = "Cijena narudžbe sa popustom iznosi: " + response + " KM";
    })
  }

  posaljiNarudzbu() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/Zakljuci/" + this.odabraniKupon.id, MyConfig.httpOpcije()).subscribe((response : any)=>{
      console.log(response);
      this.ucitajNarudzbu();
      this.zakljuciNarudzbu = false;
      document.getElementById('kolicina').innerHTML = "0";
    })
  }
}
