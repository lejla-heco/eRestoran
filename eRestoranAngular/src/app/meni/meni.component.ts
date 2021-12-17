import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MeniStavka} from "./view-models/meni-stavka-vm";
import {MyConfig} from "../my-config";
import {MeniGrupa} from "./view-models/meni-grupa-vm";
import {Router} from "@angular/router";
import {Uloga} from "../helper/uloga";
import {StavkaNarudzbe} from "../narudzba/view-models/stavka-narudzbe-vm";
import {MeniStavkaKorisnik} from "./view-models/meni-korisnik-vm";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";


@Component({
  selector: 'app-meni',
  templateUrl: './meni.component.html',
  styleUrls: ['./meni.component.css']
})
export class MeniComponent implements OnInit {
  meniStavke : MeniStavka[] = null;
  meniStavkeKorisnik : MeniStavkaKorisnik[] = null;
  meniGrupe : MeniGrupa[] = null;
  novaStavkaNarudzbe : StavkaNarudzbe = new StavkaNarudzbe();
  id : number = null;
  obrisana:boolean=false; // uklanjanje modala za brisanje nakon obrisane stavke
  loginInformacije : LoginInformacije = null;

  odabranaStavkaMenija: MeniStavka = null;

  ocijenjenaStavkaMenija:MeniStavkaKorisnik=new MeniStavkaKorisnik();//ocjenjivanje

  title = "star-angular";
  stars = [1, 2, 3, 4, 5];
  rating = 0;
  hoverState = 0;

  constructor(private httpKlijent : HttpClient, private router : Router) {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.getMeniGrupe();
    if (this.loginInformacije.isPermisijaKorisnik) this.ucitajMeniStavkeKorisnik();
    else this.ucitajMeniStavke();
  }
  public ucitajMeniStavke(kategorija : string = "Doručak") {
    this.httpKlijent.get(MyConfig.adresaServera + "/Meni/GetAllPaged?nazivKategorije=" + kategorija).subscribe((result : any)=>{
      this.meniStavke = result;
    })
  }

  public ucitajMeniStavkeKorisnik(kategorija : string = "Doručak") {
    var podaci : any = new Object();
    podaci.korisnikId = this.loginInformacije.autentifikacijaToken.korisnickiNalog.id;
    podaci.nazivKategorije = kategorija;
    this.httpKlijent.post(MyConfig.adresaServera + "/Meni/GetAllPagedLog", podaci).subscribe((result : any)=>{
      this.meniStavkeKorisnik = result;
    });
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
    this.obrisana=true;
    this.ucitajMeniStavke(s.nazivGrupe);
  });

  }

  public prikazi_brisanje(stavka : MeniStavka){
    this.odabranaStavkaMenija = stavka;
  }

  dodajUKorpu(stavka : MeniStavkaKorisnik) {
    this.novaStavkaNarudzbe.korisnikId = this.loginInformacije.autentifikacijaToken.korisnickiNalog.id;
    this.novaStavkaNarudzbe.meniStavkaId = stavka.id;
    this.httpKlijent.post(MyConfig.adresaServera+"/Narudzba/AddStavka",this.novaStavkaNarudzbe).subscribe((response : any)=>{
      document.getElementById('kolicina').innerHTML = response;
    });
  }
  prikaziOcjenjivanje(stavka : MeniStavkaKorisnik) {
    this.ocijenjenaStavkaMenija=stavka;
  }
  enter(i:any) {
    this.hoverState = i;
  }
  leave($event: number) {
    this.hoverState = 0;
  }
  updateRating(i:any) {
    this.rating = i;
    //alert("Uspješno ste ocijenili stavku "+this.id+" sa "+i+" zvjezdice");
  }
  upravljajOmiljenomStavkom(stavka : MeniStavkaKorisnik) {
    if (stavka.omiljeno){
      stavka.omiljeno = false;
      this.ukloniOmiljenuStavku(stavka);
    }
    else {
      stavka.omiljeno = true;
      this.dodajOmiljenuStavku(stavka);
    }
  }

  private ukloniOmiljenuStavku(stavka: MeniStavkaKorisnik) {
    var podaci : any = new Object();
    podaci.id = this.loginInformacije.autentifikacijaToken.korisnickiNalog.id;
    podaci.stavkaId = stavka.id;
      this.httpKlijent.post(MyConfig.adresaServera + '/OmiljenaStavka/DeleteById', podaci).subscribe((response : any)=>{
        alert("Uspjesno uklonjena omiljena stavka menija!");
      })
  }

  private dodajOmiljenuStavku(stavka: MeniStavkaKorisnik) {
    var podaci : any = new Object();
    podaci.korisnikId = this.loginInformacije.autentifikacijaToken.korisnickiNalog.id;
    podaci.meniStavkaId = stavka.id;
    this.httpKlijent.post(MyConfig.adresaServera + '/OmiljenaStavka/Add', podaci).subscribe((response : any)=>{
      alert("Dodano u omiljeno");
    });
  }
}
