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
  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

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

  naruci(narudzba : OmiljenaNarudzba) {
    this.httpKlijent.get(MyConfig.adresaServera + "/OmiljenaNarudzba/Naruci/" + narudzba.id, MyConfig.httpOpcije()).subscribe((response : any)=>{
      narudzba.status = response.status;
      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov = "Vaša narudžba je uspješno poslana";
      this.obavjestenjeSadrzaj = "Uspješno ste ponovo naručili odabranu najdražu narudžbu po cijeni od: " + narudzba.cijena + " KM";
    });
  }

  animirajObavjestenje() {
    return this.closeModal == true? 'animate__animated animate__bounceOut' : 'animate__animated animate__bounceIn';
  }

  zatvoriModalObavjestenje(){
    this.closeModal = true;
    this.animirajObavjestenje();
    this.obavjestenje = setTimeout(function (){
      return false;
    },1000)== 0? false : true;
  }

  obrisi(narudzba : OmiljenaNarudzba) {
    this.httpKlijent.get(MyConfig.adresaServera + "/OmiljenaNarudzba/Delete/" + narudzba.id, MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov = "Vaša narudžba je uspješno obrisana";
      this.obavjestenjeSadrzaj = "Uspješno ste obrisali odabranu najdražu narudžbu";
      this.ucitajNarudzbe();
    });
  }
}
