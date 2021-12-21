import { Component, OnInit } from '@angular/core';
import {MeniStavka} from "../meni/view-models/meni-stavka-vm";
import {Zaposlenik} from "./view-models/zaposlenik-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-zaposlenik',
  templateUrl: './zaposlenik.component.html',
  styleUrls: ['./zaposlenik.component.css']
})
export class ZaposlenikComponent implements OnInit {
  zaposlenici : Zaposlenik[] = null;
  odabraniZaposlenik: Zaposlenik = null;//brisanje

  id : number = null;//edit
  constructor(private httpKlijent:HttpClient,private router : Router) { }

  ngOnInit(): void {
    this.ucitajZaposlenike();
  }
  public ucitajZaposlenike() {
    this.httpKlijent.get(MyConfig.adresaServera+"/Zaposlenik/GetAll").subscribe((result : any)=>{
      this.zaposlenici = result;
      this.odabraniZaposlenik=null;
    })
  }

  prikazi_brisanje(zaposlenik:Zaposlenik) {
    this.odabraniZaposlenik = zaposlenik;
  }

  brisanje(zaposlenik: Zaposlenik) {
    this.httpKlijent.post(MyConfig.adresaServera+"/Zaposlenik/Delete/"+zaposlenik.id, zaposlenik).subscribe((x:any)=>{
      alert("Zaposlenik "+ zaposlenik.ime+zaposlenik.prezime+ " je uspješno obrisan");

      this.ucitajZaposlenike();
    });
  }

  detalji(id:number) {
    this.router.navigate(['/edit-zaposlenik', id]);
  }


}
