import { Component, OnInit } from '@angular/core';
import {MojaNarudzba} from "../moje-narudzbe/view-models/moja-narudzba-vm";
import {Router} from "@angular/router";
import {Narudzba} from "./view-model/narudzbe-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {NarudzbaStatus} from "./view-model/status-narudzbe-vm";
import {UrediStatusNarudzbe} from "./view-model/narudzba-status-uredu-vm";

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


  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  constructor(private httpKlijent:HttpClient,private router: Router) { }

  ngOnInit(): void {
    this.ucitajNarudzbe();
    this.getAllStatusiNarudzbe();
  }
  private ucitajNarudzbe() {
    this.httpKlijent.get("https://localhost:44325" + "/Narudzba/GetAllPagedZaposlenik/" + this.currentPage,MyConfig.httpOpcije()).subscribe((response : any)=>{
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
    this.ucitajNarudzbe();
  }
  reloadPage() {
    this.router.navigate(["/pregled-narudzbi"])
  }

  urediStatus(narudzba:Narudzba) {
   this.urediStatusNarudzbe.id=narudzba.id;
   this.urediStatusNarudzbe.statusID=narudzba.statusID;
  // this.urediStatusNarudzbe.status=narudzba.status;

    this.httpKlijent.post("https://localhost:44325"+"/Narudzba/UpdateStatusZaposlenik/"+this.urediStatusNarudzbe.id,this.urediStatusNarudzbe,MyConfig.httpOpcije()).subscribe((result:any)=>{

      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov ="Uređen status";
      this.obavjestenjeSadrzaj="Uspješno ste uredili status narudzbe";
      this.ucitajNarudzbe();
    })

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


}
