import { Component, OnInit } from '@angular/core';
import {MojaNarudzba} from "../moje-narudzbe/view-models/moja-narudzba-vm";
import {Router} from "@angular/router";
import {Narudzba} from "./view-model/narudzbe-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {NarudzbaStatus} from "./view-model/status-narudzbe-vm";
import {UrediStatusNarudzbe} from "./view-model/narudzba-status-uredu-vm";
import {Dostavljac} from "../dostavljac/view-models/dostavljac-vm";

@Component({
  selector: 'app-pregled-narudzbi',
  templateUrl: './pregled-narudzbi.component.html',
  styleUrls: ['./pregled-narudzbi.component.css']
})
export class PregledNarudzbiComponent implements OnInit {
  currentPage: number = 1;
  totalPages: number;
  mojeNarudzbe : Narudzba[] = null;
statusi:NarudzbaStatus[]=null;

urediStatusNarudzbe:UrediStatusNarudzbe= new UrediStatusNarudzbe();

  dostavljaci : Dostavljac[] = null;

  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  constructor(private httpKlijent:HttpClient,private router: Router) { }

  ngOnInit(): void {
    this.ucitajNarudzbe();
    this.getAllStatusiNarudzbe();
    this.ucitajDostavljace();
  }
  public ucitajDostavljace() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Dostavljac/GetAll").subscribe((result : any)=>{
      this.dostavljaci = result;

    })
  }
  private ucitajNarudzbe() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/GetAllPagedZaposlenik/" + this.currentPage,MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.totalPages = response.totalPages;
      this.mojeNarudzbe = response.dataItems;
    })
  }
  private getAllStatusiNarudzbe() {
    this.httpKlijent.get(MyConfig.adresaServera + "/StatusNarudzbe/GetAll", MyConfig.httpOpcije()).subscribe((result:any)=>{
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
    this.ucitajNarudzbe();
  }


  urediStatus(narudzba:Narudzba) {
   this.urediStatusNarudzbe.id=narudzba.id;
   this.urediStatusNarudzbe.statusID=narudzba.statusID;
  // this.urediStatusNarudzbe.status=narudzba.status;

    if(this.urediStatusNarudzbe.statusID==4 && (this.dostavljaci == null || this.dostavljaci?.length == 0 ))//Spremljeno
    {
      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov ="Upozorenje";
      this.obavjestenjeSadrzaj="Trenutno nemate dostavlja??a kojim bi dodijelili narudzbu";
    }
    this.httpKlijent.post(MyConfig.adresaServera + "/Narudzba/UpdateStatusZaposlenik/"+this.urediStatusNarudzbe.id,this.urediStatusNarudzbe,MyConfig.httpOpcije()).subscribe((result:any)=>{

      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov ="Ure??en status";
      this.obavjestenjeSadrzaj="Uspje??no ste uredili status narudzbe";
      this.ucitajNarudzbe();
    })

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
