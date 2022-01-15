import { Component, OnInit } from '@angular/core';
import {Zaposlenik} from "../zaposlenik/view-models/zaposlenik-vm";
import {Dostavljac} from "./view-models/dostavljac-vm";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MyConfig} from "../my-config";

@Component({
  selector: 'app-dostavljac',
  templateUrl: './dostavljac.component.html',
  styleUrls: ['./dostavljac.component.css']
})
export class DostavljacComponent implements OnInit {
  dostavljaci : Dostavljac[] = null;
  odabraniDostavljac: Dostavljac = null;//brisanje


  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  id : number = null;//edit
  constructor(private httpKlijent:HttpClient,private router : Router) { }

  ngOnInit(): void {
    this.ucitajDostavljace();
  }
  public ucitajDostavljace() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Dostavljac/GetAll").subscribe((result : any)=>{
      this.dostavljaci = result;
      this.odabraniDostavljac=null;
    })
  }

  posaljiPodatke(id:number) {
    this.router.navigate(['/edit-dostavljac', id]);
  }

  prikazi_brisanje(odabrani: Dostavljac) {
    this.odabraniDostavljac=odabrani;
  }

  obrisi(dostavljac: Dostavljac) {
    this.httpKlijent.get(MyConfig.adresaServera+"/Dostavljac/Delete/"+dostavljac.id, MyConfig.httpOpcije()).subscribe((x:any)=>{
      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov = "Brisanje podataka uspješno";
      this.obavjestenjeSadrzaj = "Uspješno ste obrisali dostavljača "+ dostavljac.ime+" "+dostavljac.prezime;

      this.ucitajDostavljace();
    });
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
