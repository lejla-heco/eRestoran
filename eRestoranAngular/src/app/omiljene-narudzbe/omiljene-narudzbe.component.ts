import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";
import {OmiljenaNarudzba} from "./view-models/omiljena-narudzba-vm";

@Component({
  selector: 'app-omiljene-narudzbe',
  templateUrl: './omiljene-narudzbe.component.html',
  styleUrls: ['./omiljene-narudzbe.component.css']
})
export class OmiljeneNarudzbeComponent implements OnInit {
  currentPage: number = 1;
  totalPages: number;
  najdrazeNarudzbe : OmiljenaNarudzba[] = null;

  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {
    this.ucitajNarudzbe();
  }

  private ucitajNarudzbe() {
    this.httpKlijent.get(MyConfig.adresaServera + "/OmiljenaNarudzba/GetAllPaged/" + this.currentPage,MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.totalPages = response.totalPages;
      this.najdrazeNarudzbe = response.dataItems;
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
    this.ucitajNarudzbe();
  }
}
