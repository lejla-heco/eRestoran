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
    this.httpKlijent.post(MyConfig.adresaServera+"/Dostavljac/Delete/"+dostavljac.id, dostavljac).subscribe((x:any)=>{
      alert("Dostavljac "+ dostavljac.ime+dostavljac.prezime+ " je uspješno obrisan");

      this.ucitajDostavljace();
    });
  }
}
