import { Component, OnInit } from '@angular/core';
import {Narudzba} from "./view-models/narudzba-vm";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";

@Component({
  selector: 'app-narudzba',
  templateUrl: './narudzba.component.html',
  styleUrls: ['./narudzba.component.css']
})
export class NarudzbaComponent implements OnInit {
  narudzba : Narudzba = null;
  id : number;

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
}
