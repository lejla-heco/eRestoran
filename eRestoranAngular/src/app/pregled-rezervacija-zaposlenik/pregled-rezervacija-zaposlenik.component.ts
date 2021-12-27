import { Component, OnInit } from '@angular/core';
import {NarudzbaDostavljac} from "../pregled-narudzbi-dostavljac/view-models/narudzbe-dostavljac-vm";
import {NarudzbaStatus} from "../pregled-narudzbi/view-model/status-narudzbe-vm";
import {RezervacijaZaposlenik} from "./view-models/rezervacije-zaposlenik";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {RezervacijaStatus} from "./view-models/rezervacija-status";

@Component({
  selector: 'app-pregled-rezervacija-zaposlenik',
  templateUrl: './pregled-rezervacija-zaposlenik.component.html',
  styleUrls: ['./pregled-rezervacija-zaposlenik.component.css']
})
export class PregledRezervacijaZaposlenikComponent implements OnInit {
  currentPage: number = 1;
  totalPages: number;

  rezervacije : RezervacijaZaposlenik[] = null;
  statusi:RezervacijaStatus[]=null;
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.ucitajRezervacije();
    this.getAllStatusiRezervacije();
  }
  private ucitajRezervacije() {
    this.httpKlijent.get("https://localhost:44325" + "/Rezervacija/GetAllPagedZaposlenik/" + this.currentPage,MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.totalPages = response.totalPages;
      this.rezervacije = response.dataItems;
    })
  }
  private getAllStatusiRezervacije() {
    this.httpKlijent.get("https://localhost:44325" + "/StatusRezervacije/GetAll").subscribe((result:any)=>{
      this.statusi = result;
    });
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
