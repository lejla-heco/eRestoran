import { Component, OnInit } from '@angular/core';
import {Uloga} from "../helper/uloga";
import {OmiljenaStavka} from "./view-models/omiljena-stavka-vm";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";
import {MeniGrupa} from "../meni/view-models/meni-grupa-vm";

@Component({
  selector: 'app-omiljenje-stavke',
  templateUrl: './omiljenje-stavke.component.html',
  styleUrls: ['./omiljenje-stavke.component.css']
})
export class OmiljenjeStavkeComponent implements OnInit {
  omiljeneStavke: OmiljenaStavka[] = null;
  private korisnikId: number;
  public meniGrupe: MeniGrupa[] = null;

  constructor(private httpKlijent : HttpClient) {
    if (sessionStorage.getItem("autentifikacija-token") || localStorage.getItem("autentifikacija-token")) {
      var korisnik = JSON.parse(sessionStorage.getItem("autentifikacija-token"));
      if (korisnik == null) korisnik = JSON.parse(localStorage.getItem("autentifikacija-token"));
      this.korisnikId = korisnik.korisnickiNalog.id;
    }
  }

  ngOnInit(): void {
    this.getMeniGrupe();
    this.ucitajOmiljeneStavke();
  }

  ucitajOmiljeneStavke(kategorija : string = "Doručak") {
    var podaci : any = new Object();
    podaci.id = this.korisnikId;
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
}
