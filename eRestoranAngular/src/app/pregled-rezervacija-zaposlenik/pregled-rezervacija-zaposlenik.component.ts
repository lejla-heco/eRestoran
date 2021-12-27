import { Component, OnInit } from '@angular/core';
import {NarudzbaDostavljac} from "../pregled-narudzbi-dostavljac/view-models/narudzbe-dostavljac-vm";
import {NarudzbaStatus} from "../pregled-narudzbi/view-model/status-narudzbe-vm";
import {RezervacijaZaposlenik} from "./view-models/rezervacije-zaposlenik";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-pregled-rezervacija-zaposlenik',
  templateUrl: './pregled-rezervacija-zaposlenik.component.html',
  styleUrls: ['./pregled-rezervacija-zaposlenik.component.css']
})
export class PregledRezervacijaZaposlenikComponent implements OnInit {
  currentPage: number = 1;
  totalPages: number;

  rezervacije : RezervacijaZaposlenik[] = null;
  //statusi:NarudzbaStatus[]=null;
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.ucitajRezervacije();
  }
  private ucitajRezervacije() {
    this.httpKlijent.get("https://localhost:44325" + "/Rezervacija/GetAllPagedZaposlenik/" + this.currentPage,MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.totalPages = response.totalPages;
      this.rezervacije = response.dataItems;
    })
  }
  createRangeStranica() {
    var niz = new Array(this.totalPages);
    for(let i : number = 0; i < this.totalPages; i++){
      niz[i] = i + 1;
    }
    return niz;
  }

  ucitajStranicu(page : number) {
    this.currentPage = page;
   this.ucitajRezervacije();
  }


}
