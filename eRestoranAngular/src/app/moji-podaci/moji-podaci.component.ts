import { Component, OnInit } from '@angular/core';
import {Korisnik} from "./view-models/korisnik-vm";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";
import {Opstina} from "../registracija/view-models/opstina-vm";

@Component({
  selector: 'app-moji-podaci',
  templateUrl: './moji-podaci.component.html',
  styleUrls: ['./moji-podaci.component.css']
})
export class MojiPodaciComponent implements OnInit {
  korisnik : Korisnik = new Korisnik();
  opstine : Opstina[] = null;
  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {
    this.getOpstine();
    this.ucitajPodatke();
  }

  private ucitajPodatke() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Korisnik/Get", MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.korisnik = response;
    });
  }

  private getOpstine() {
    this.httpKlijent.get( MyConfig.adresaServera + "/Opstina/GetAll").subscribe((response : any)=>{
      this.opstine = response;
    });
  }
}
