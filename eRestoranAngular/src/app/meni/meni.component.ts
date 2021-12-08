import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MeniStavka} from "./view-models/meni-stavka-vm";
import {MyConfig} from "../my-config";
import {MeniGrupa} from "./view-models/meni-grupa-vm";
import {NovaMeniStavka} from "./view-models/nova-meni-stavka-vm";
import {Router} from "@angular/router";

@Component({
  selector: 'app-meni',
  templateUrl: './meni.component.html',
  styleUrls: ['./meni.component.css']
})
export class MeniComponent implements OnInit {
  meniStavke : MeniStavka[] = null;
  meniGrupe : MeniGrupa[] = null;
  odabranaStavka: NovaMeniStavka = new NovaMeniStavka();


  constructor(private httpKlijent : HttpClient, private router : Router) { }

  ngOnInit(): void {
    this.getMeniGrupe();
    this.ucitajMeniStavke();
  }
  public ucitajMeniStavke(kategorija : string = "Doručak") {
    this.httpKlijent.get(MyConfig.adresaServera + "/Meni/GetAllPaged?nazivKategorije=" + kategorija).subscribe((result : any)=>{
      this.meniStavke = result;
    })
  }

  createRange(ocjena: number) {
    let velicina = Math.round(ocjena);
    return new Array(velicina);
  }

  private getMeniGrupe() {
    this.httpKlijent.get(MyConfig.adresaServera + "/MeniGrupa/GetAll").subscribe((result : any)=>{
      this.meniGrupe = result;
    })
  }

  izdvoji(stavka : MeniStavka) {
    this.httpKlijent.post(MyConfig.adresaServera + "/PosebnaPonuda/Izdvoji", stavka.id).subscribe((result : any)=>{
      this.ucitajMeniStavke(stavka.nazivGrupe);
    });
  }

  detalji(id : number) {
    this.router.navigate(['/edit-stavka', id]);

  }

  brisanje(s :MeniStavka) {
    //this.router.navigate(['/delete-stavka', id]);
if(confirm("Da li želite obrisati odabranu stavku "+s.naziv)){
  this.httpKlijent.post(MyConfig.adresaServera+"/Meni/Delete/"+s.id, s).subscribe((x:any)=>{
    alert("Stavka "+ s.naziv+ " je uspješno obrisana");
  })
  }
}


}
