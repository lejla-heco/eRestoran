import { Component, OnInit } from '@angular/core';
import {PosebnaPonudaStavka} from "./view-models/posebna-ponuda-stavka-vm";
import {HttpClient} from "@angular/common/http";
import { MyConfig } from '../my-config';
import {Router} from "@angular/router";
import {Uloga} from "../helper/uloga";
import {NovaStavkaNarudzbe} from "../narudzba/view-models/nova-stavka-narudzbe-vm";

@Component({
  selector: 'app-posebna-ponuda',
  templateUrl: './posebna-ponuda.component.html',
  styleUrls: ['./posebna-ponuda.component.css']
})
export class PosebnaPonudaComponent implements OnInit {
  posebnaPonuda : PosebnaPonudaStavka[] = null;
  odabranaStavka: PosebnaPonudaStavka = null;
  uloga: string;
  id : number;
  novaStavkaNarudzbe : NovaStavkaNarudzbe = new NovaStavkaNarudzbe();

  constructor(private httpKlijent : HttpClient, private router : Router) {
    if (sessionStorage.getItem("autentifikacija-token") || localStorage.getItem("autentifikacija-token")) {
      var korisnik = JSON.parse(sessionStorage.getItem("autentifikacija-token"));
      if (korisnik == null) korisnik = JSON.parse(localStorage.getItem("autentifikacija-token"));
      this.uloga = korisnik.korisnickiNalog.uloga.naziv;
      this.id = korisnik.korisnickiNalog.id;
    }
    else this.uloga = Uloga.GOST;
  }

  ngOnInit(): void {
    this.getPosebnaPonuda();
  }

  private getPosebnaPonuda() {
    this.httpKlijent.get(MyConfig.adresaServera + "/PosebnaPonuda/GetAll").subscribe((result:any) =>{
      this.posebnaPonuda = result;
    })
  }

  public prikaziDetalje(stavka : PosebnaPonudaStavka){
    this.odabranaStavka = stavka;
  }

  ukloni(id : number) {
    this.httpKlijent.post(MyConfig.adresaServera + "/PosebnaPonuda/Ukloni", id).subscribe((result : any)=>{
      alert("Uklonjena stavka posebne ponude");
      this.getPosebnaPonuda();
    });
  }

  createRange(ocjena: number) {
    let velicina = Math.round(ocjena);
    return new Array(velicina);
  }

  dodajUKorpu(stavka: PosebnaPonudaStavka) {
    this.novaStavkaNarudzbe.korisnikId = this.id;
    this.novaStavkaNarudzbe.meniStavkaId = stavka.id;
    this.httpKlijent.post(MyConfig.adresaServera+"/Narudzba/AddStavka",this.novaStavkaNarudzbe).subscribe((response : any)=>{
      document.getElementById('kolicina').innerHTML = response;
    });
  }
}
