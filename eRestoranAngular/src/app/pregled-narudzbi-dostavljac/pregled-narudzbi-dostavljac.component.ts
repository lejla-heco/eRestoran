import { Component, OnInit } from '@angular/core';
import {Narudzba} from "../pregled-narudzbi/view-model/narudzbe-vm";
import {NarudzbaStatus} from "../pregled-narudzbi/view-model/status-narudzbe-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {NarudzbaDostavljac} from "./view-models/narudzbe-dostavljac-vm";
import {UrediStatusNarudzbe} from "../pregled-narudzbi/view-model/narudzba-status-uredu-vm";

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

  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  urediStatusNarudzbe:UrediStatusNarudzbe= new UrediStatusNarudzbe();
  constructor(private httpKlijent :HttpClient) {

  }

  ngOnInit(): void {

    this.getAllStatusiNarudzbe();
    this.ucitajNarudzbe();

  }
  private ucitajNarudzbe() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/GetAllPagedDostavljac/" + this.currentPage,MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.totalPages = response.totalPages;
      this.mojeNarudzbe = response.dataItems;
    })
  }
  private getAllStatusiNarudzbe() {
    this.httpKlijent.get(MyConfig.adresaServera + "/StatusNarudzbe/GetAll", MyConfig.httpOpcije()).subscribe((result:any)=>{
      this.statusi = result;
    });
  }
  urediStatus(narudzba:NarudzbaDostavljac) {
    this.urediStatusNarudzbe.id=narudzba.id;
    this.urediStatusNarudzbe.statusID=narudzba.statusID;
    // this.urediStatusNarudzbe.status=narudzba.status;

    this.httpKlijent.post(MyConfig.adresaServera + "/Narudzba/UpdateStatusDostavljac/"+this.urediStatusNarudzbe.id,this.urediStatusNarudzbe,MyConfig.httpOpcije()).subscribe((result:any)=>{

      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov ="Uređen status";
      this.obavjestenjeSadrzaj="Uspješno ste uredili status narudzbe";
      this.ucitajNarudzbe();
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
   // this.ucitajNarudzbe();
  }
  animirajObavjestenje() {
    return this.closeModal == true? 'animate__animated animate__bounceOut' : 'animate__animated animate__bounceIn';
  }

  zatvoriModalObavjestenje(){
    this.closeModal = true;
    this.animirajObavjestenje();
    setTimeout(() => {
      this.obavjestenje = false;
    },1000);
  }
}
