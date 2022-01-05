import { Component, OnInit } from '@angular/core';
import {Uloga} from "../helper/uloga";
import {OmiljenaStavka} from "./view-models/omiljena-stavka-vm";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";
import {MeniGrupa} from "../meni/view-models/meni-grupa-vm";
import {StavkaNarudzbe} from "../narudzba/view-models/stavka-narudzbe-vm";

@Component({
  selector: 'app-omiljenje-stavke',
  templateUrl: './omiljenje-stavke.component.html',
  styleUrls: ['./omiljenje-stavke.component.css']
})
export class OmiljenjeStavkeComponent implements OnInit {
  omiljeneStavke: OmiljenaStavka[] = null;
  public meniGrupe: MeniGrupa[] = null;
  private novaStavkaNarudzbe : StavkaNarudzbe = new StavkaNarudzbe();
  totalPages : number;
  trenutnaKategorija : string;
  currentPage : number;
  itemsPerPage : number = 4;
  pageNumber : number = 1;

  constructor(private httpKlijent : HttpClient) {
  }

  ngOnInit(): void {
    this.getMeniGrupe();
    this.ucitajOmiljeneStavke();
  }

  ucitajOmiljeneStavke(kategorija : string = "Doručak", pageNumber : number = 1) {
    this.trenutnaKategorija = kategorija;
    var podaci : any = {
      kategorija : kategorija,
      itemsPerPage : this.itemsPerPage,
      pageNumber : pageNumber
    };
    this.httpKlijent.post(MyConfig.adresaServera + '/OmiljenaStavka/GetAllPaged', podaci, MyConfig.httpOpcije()).subscribe((response : any)=> {
      //console.log(response);
      this.omiljeneStavke = response.dataItems;
      this.totalPages = response.totalPages;
      this.currentPage = response.currentPage;
    })
  }

  createRange(ocjena: number) {
    let velicina = Math.round(ocjena);
    return new Array(velicina);
  }

  createRangeStranica() {
    var niz = new Array(this.totalPages);
    for(let i : number = 0; i < this.totalPages; i++){
      niz[i] = i + 1;
    }
    return niz;
  }

  private getMeniGrupe() {
    this.httpKlijent.get(MyConfig.adresaServera + "/MeniGrupa/GetAll").subscribe((result : any)=>{
      this.meniGrupe = result;
    })
  }

  dodajUKorpu(stavka : OmiljenaStavka) {
    this.novaStavkaNarudzbe.meniStavkaId = stavka.id;
    this.httpKlijent.post(MyConfig.adresaServera+"/Narudzba/AddStavka",this.novaStavkaNarudzbe, MyConfig.httpOpcije()).subscribe((response : any)=>{
      document.getElementById('kolicina').innerHTML = response;
    });
  }

  obrisiStavku(stavka : OmiljenaStavka) {
    this.httpKlijent.get(MyConfig.adresaServera+"/OmiljenaStavka/Delete/" + stavka.omiljenaStavkaId, MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.ucitajOmiljeneStavke(stavka.nazivGrupe);
    });
  }

  ucitajStranicu(page : number) {
    this.ucitajOmiljeneStavke(this.trenutnaKategorija, page);
  }

  ucitajStavke() {
    this.ucitajOmiljeneStavke(this.trenutnaKategorija);
  }
}
