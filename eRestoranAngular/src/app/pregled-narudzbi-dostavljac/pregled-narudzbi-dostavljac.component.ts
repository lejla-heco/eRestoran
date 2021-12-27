import { Component, OnInit } from '@angular/core';
import {Narudzba} from "../pregled-narudzbi/view-model/narudzbe-vm";
import {NarudzbaStatus} from "../pregled-narudzbi/view-model/status-narudzbe-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {NarudzbaDostavljac} from "./view-models/narudzbe-dostavljac-vm";

@Component({
  selector: 'app-pregled-narudzbi-dostavljac',
  templateUrl: './pregled-narudzbi-dostavljac.component.html',
  styleUrls: ['./pregled-narudzbi-dostavljac.component.css']
})
export class PregledNarudzbiDostavljacComponent implements OnInit {
  currentPage: number = 1;
  totalPages: number;

  mojeNarudzbe : NarudzbaDostavljac[] = null;
  statusi:NarudzbaStatus[]=null;
  constructor(private httpKlijent :HttpClient) {

  }

  ngOnInit(): void {

    this.getAllStatusiNarudzbe();
    this.ucitajNarudzbe();

  }
  private ucitajNarudzbe() {
    this.httpKlijent.get("https://localhost:44325" + "/Narudzba/GetAllPagedDostavljac/" + this.currentPage,MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.totalPages = response.totalPages;
      this.mojeNarudzbe = response.dataItems;
    })
  }
  private getAllStatusiNarudzbe() {
    this.httpKlijent.get(MyConfig.adresaServera + "/StatusNarudzbe/GetAll").subscribe((result:any)=>{
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
   // this.ucitajNarudzbe();
  }
}
