import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MeniStavka} from "./view-models/meni-stavka-vm";
import {MyConfig} from "../my-config";
import {MeniGrupa} from "./view-models/meni-grupa-vm";
import {Router} from "@angular/router";
import {StavkaNarudzbe} from "../narudzba/view-models/stavka-narudzbe-vm";
import {MeniStavkaKorisnik} from "./view-models/meni-korisnik-vm";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {AngularFireDatabase} from "@angular/fire/compat/database";


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

  loginInformacije : LoginInformacije = null;
  odabranaStavkaMenija: MeniStavka = null;
  ocijenjenaStavkaMenija:MeniStavkaKorisnik=new MeniStavkaKorisnik();
  trenutnaKategorija: string = "Doručak";
  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  title = "star-angular";
  stars = [1, 2, 3, 4, 5];
  rating = 0;
  hoverState = 0;

  constructor(private httpKlijent : HttpClient, private router : Router, private db : AngularFireDatabase) {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.getMeniGrupe();
    if (this.loginInformacije.isPermisijaKorisnik) this.ucitajMeniStavkeKorisnik();
    else this.ucitajMeniStavke();
  }
  public ucitajMeniStavke(kategorija : string = "Doručak") {
    this.trenutnaKategorija = kategorija;
    this.httpKlijent.get(MyConfig.adresaServera + "/Meni/GetAllPaged?nazivKategorije=" + kategorija).subscribe((result : any)=>{
      this.meniStavke = result;
      this.odabranaStavkaMenija=null;
    })
  }

  public ucitajMeniStavkeKorisnik(kategorija : string = "Doručak") {
    this.trenutnaKategorija = kategorija;
    let podaci : any = {
      kategorija : kategorija
    };
    this.httpKlijent.post(MyConfig.adresaServera + "/Meni/GetAllPagedLog", podaci, MyConfig.httpOpcije()).subscribe((result : any)=>{
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
    this.httpKlijent.post(MyConfig.adresaServera + "/PosebnaPonuda/Izdvoji", stavka.id, MyConfig.httpOpcije()).subscribe((result : any)=>{
      this.ucitajMeniStavke(stavka.nazivGrupe);
      this.db.list("PosebnaPonuda").set("NazivStavke",stavka.naziv);
      this.db.list("PosebnaPonuda").set("Cijena",stavka.snizenaCijena);
      this.db.list("PosebnaPonuda").set("Notifikacija",true);
      setTimeout(()=>{
        this.db.list("PosebnaPonuda").set("Notifikacija",false);
      },650);
    });
  }

  detalji(id : number) {
    this.router.navigate(['/edit-stavka', id]);
  }



    brisanje(s : MeniStavka) {
      console.log(s);
      this.httpKlijent.get(MyConfig.adresaServera+ "/Meni/Delete/" + s.id, MyConfig.httpOpcije()).subscribe((response : any)=>{
        this.zatvoriModal();
        this.ucitajMeniStavke(s.nazivGrupe);
        this.obavjestenje = true;
        this.closeModal = false;
        this.obavjestenjeNaslov="Uspješno obrisana meni stavka";
        this.obavjestenjeSadrzaj="Uspješno ste obrisali meni stavku  "+s.naziv;
      });
    }



  public prikazi_brisanje(stavka : MeniStavka){
    this.odabranaStavkaMenija = stavka;
    this.closeModal = false;
  }

  dodajUKorpu(stavka : MeniStavkaKorisnik) {
    this.novaStavkaNarudzbe.meniStavkaId = stavka.id;
    this.httpKlijent.post(MyConfig.adresaServera+"/Narudzba/AddStavka",this.novaStavkaNarudzbe, MyConfig.httpOpcije()).subscribe((response : any)=>{
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
    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = "Vaša ocjena je uspješno poslana";
    this.obavjestenjeSadrzaj = this.ocijenjenaStavkaMenija.naziv + " stavku ste ocijenili ocjenom " + this.rating;
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
    var podaci : any = {
      stavkaId : stavka.id
    };
      this.httpKlijent.post(MyConfig.adresaServera + '/OmiljenaStavka/DeleteById', podaci, MyConfig.httpOpcije()).subscribe((response : any)=>{

      });
  }

  private dodajOmiljenuStavku(stavka: MeniStavkaKorisnik) {
    var podaci : any = {
      meniStavkaId : stavka.id
    };
    this.httpKlijent.post(MyConfig.adresaServera + '/OmiljenaStavka/Add', podaci, MyConfig.httpOpcije()).subscribe((response : any)=>{

    });
    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = "Stavka dodana u sekciju omiljenih stavki";
    this.obavjestenjeSadrzaj = stavka.naziv + " stavka je dodana u omiljene";
  }

  animirajObavjestenje() {
    return this.closeModal == true? 'animate__animated animate__bounceOut' : 'animate__animated animate__bounceIn';
  }

  zatvoriModalObavjestenje(){
    this.closeModal = true;
    this.animirajObavjestenje();
    setTimeout(()=>{
      this.obavjestenje = false;
    },500);
  }

  zatvoriModal(){
    this.closeModal = true;
    this.animirajObavjestenje();

    setTimeout(()=>{
      this.odabranaStavkaMenija = null;
    },1000);
  }
}
