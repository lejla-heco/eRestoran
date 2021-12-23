import { Component, OnInit } from '@angular/core';
import {Zaposlenik} from "../../zaposlenik/view-models/zaposlenik-vm";
import {MyConfig} from "../../my-config";
import {HttpClient} from "@angular/common/http";
import {Rezervacija} from "../view-models/rezervacija-vm";
import {Prigoda} from "../view-models/prigoda-vm";

@Component({
  selector: 'app-pregled-rezervacija',
  templateUrl: './pregled-rezervacija.component.html',
  styleUrls: ['./pregled-rezervacija.component.css']
})
export class PregledRezervacijaComponent implements OnInit {

  rezervacije : Rezervacija[] = null;

  odabranaRezervacija: Rezervacija = null;//brisanje
  //obrisana:boolean=false;
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {

    this.ucitajRezervacije();
  }

  public ucitajRezervacije() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Rezervacija/GetAll").subscribe((result : any)=>{
      this.rezervacije = result;
      this.odabranaRezervacija=null;
    });
  }


  prikaziBrisanje(rezervacija: Rezervacija) {
    this.odabranaRezervacija=rezervacija;
  }

  brisanje(rezervacija: Rezervacija) {

      this.httpKlijent.post(MyConfig.adresaServera + "/Rezervacija/Delete/"+rezervacija.id, rezervacija).subscribe((x:any)=>{
        alert("Odabrana rezervacija je uspješno otkazana");
        this.ucitajRezervacije();
      });

  }
}
