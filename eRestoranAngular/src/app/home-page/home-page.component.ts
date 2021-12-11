import { Component, OnInit } from '@angular/core';
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {PosebnaPonudaStavka} from "../posebna-ponuda/view-models/posebna-ponuda-stavka-vm";
import {Uloga} from "../helper/uloga";

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  public posebnaPonuda: PosebnaPonudaStavka[] = null;

  constructor(private httpKlijent : HttpClient) {
    if (sessionStorage.getItem("autentifikacija-token") || localStorage.getItem("autentifikacija-token")) {
      var korisnik = JSON.parse(sessionStorage.getItem("autentifikacija-token"));
      if(korisnik == null) korisnik = JSON.parse(localStorage.getItem("autentifikacija-token"));
      if (korisnik.korisnickiNalog.uloga.naziv == Uloga.KORISNIK) this.ucitajBrojStavki(korisnik.korisnickiNalog.id);
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
