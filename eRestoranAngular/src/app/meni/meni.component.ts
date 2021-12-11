import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MeniStavka} from "./view-models/meni-stavka-vm";
import {MyConfig} from "../my-config";
import {MeniGrupa} from "./view-models/meni-grupa-vm";
import {NovaMeniStavka} from "./view-models/nova-meni-stavka-vm";
import {Router} from "@angular/router";
import {askConfirmation} from "@angular/cli/utilities/prompt";
import {PosebnaPonudaStavka} from "../posebna-ponuda/view-models/posebna-ponuda-stavka-vm";
import {Uloga} from "../helper/uloga";
import {NovaStavkaNarudzbe} from "../narudzba/view-models/nova-stavka-narudzbe-vm";

@Component({
  selector: 'app-meni',
  templateUrl: './meni.component.html',
  styleUrls: ['./meni.component.css']
})
export class MeniComponent implements OnInit {
  meniStavke : MeniStavka[] = null;
  meniGrupe : MeniGrupa[] = null;
  odabranaStavka: NovaMeniStavka = new NovaMeniStavka();
  uloga : string = null;
  id : number = null;
  novaStavkaNarudzbe : NovaStavkaNarudzbe = new NovaStavkaNarudzbe();

  odabranaStavkaMenija: MeniStavka = null;


  constructor(private httpKlijent : HttpClient, private router : Router) {
    if (sessionStorage.getItem("autentifikacija-token") || localStorage.getItem("autentifikacija-token")) {
      var korisnik = JSON.parse(sessionStorage.getItem("autentifikacija-token"));
      if(korisnik == null) korisnik = JSON.parse(localStorage.getItem("autentifikacija-token"));
      this.uloga = korisnik.korisnickiNalog.uloga.naziv;
      this.id = korisnik.korisnickiNalog.id;
    }
    else this.uloga = Uloga.GOST;
  }

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

  this.httpKlijent.post(MyConfig.adresaServera+"/Meni/Delete/"+s.id, s).subscribe((x:any)=>{
    alert("Stavka "+ s.naziv+ " je uspješno obrisana");

  });

  }


  public prikazi_brisanje(stavka : MeniStavka){
    this.odabranaStavkaMenija = stavka;
  }

  dodajUKorpu(stavka : MeniStavka) {
    this.novaStavkaNarudzbe.korisnikId = this.id;
    this.novaStavkaNarudzbe.meniStavkaId = stavka.id;
    this.httpKlijent.post(MyConfig.adresaServera+"/Narudzba/AddStavka",this.novaStavkaNarudzbe).subscribe((response : any)=>{
      document.getElementById('kolicina').innerHTML = response;
    });
  }
}
