import { Component, OnInit } from '@angular/core';
import {MeniStavka} from "../meni/view-models/meni-stavka-vm";
import {Zaposlenik} from "./view-models/zaposlenik-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {AutentifikacijaToken} from "../helper/autentifikacija-token";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

@Component({
  selector: 'app-zaposlenik',
  templateUrl: './zaposlenik.component.html',
  styleUrls: ['./zaposlenik.component.css']
})
export class ZaposlenikComponent implements OnInit {
  zaposlenici : Zaposlenik[] = null;
  odabraniZaposlenik: Zaposlenik = null;//brisanje

  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

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
    this.httpKlijent.get(MyConfig.adresaServera+"/Zaposlenik/Delete/"+zaposlenik.id,MyConfig.httpOpcije()).subscribe((x:any)=>{
      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov = "Brisanje uspješno uspješno";
      this.obavjestenjeSadrzaj = "Uspješno ste obrisali zaposlenika "+ zaposlenik.ime+" "+zaposlenik.prezime;

      this.ucitajZaposlenike();
    });
  }

  detalji(id:number) {
    this.router.navigate(['/edit-zaposlenik', id]);
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

  generisiIzvjestaj() {
    let autentifikacijaToken:AutentifikacijaToken = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken;
    let mojtoken = "";

    if (autentifikacijaToken!=null)
      mojtoken = autentifikacijaToken.vrijednost;

    this.httpKlijent.get(MyConfig.adresaServera+"/ReportZaposlenik/PreuzmiIzvjestaj", {
      responseType: 'blob',
      headers: { 'autentifikacija-token': mojtoken, }
    }).subscribe((response:any)=>{
      console.log(response);
      var link = window.URL.createObjectURL(response);
      window.open(link);
    });
  }
}
