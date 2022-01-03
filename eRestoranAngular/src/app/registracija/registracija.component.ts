import { Component, OnInit } from '@angular/core';
import {Login} from "../login/view-models/login-vm";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Registracija} from "./view-models/registracija-vm";
import {MeniGrupa} from "../meni/view-models/meni-grupa-vm";
import {MyConfig} from "../my-config";
import {Opstina} from "./view-models/opstina-vm";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {

  registracija : Registracija = new Registracija();
  opstine : Opstina[] = null;
  sifra:string;
  prijava : Login = new Login();

  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  loginInformacije : LoginInformacije = null;
  constructor(private httpKlijent : HttpClient, private router : Router) {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
  }



  ngOnInit(): void {
    this.getAllOpstine();

  }
  private getAllOpstine() {
    this.httpKlijent.get( MyConfig.adresaServera + "/Opstina/GetAll").subscribe((result:any)=>{
      this.opstine = result;
    });
  }

  registracijaPodataka() {
    if(this.registracija.password==this.sifra && this.validirajFormu()){


    this.registracija.opstinaId = parseInt(this.registracija.opstinaId.toString());
  this.httpKlijent.post(MyConfig.adresaServera + "/Korisnik/Add",this.registracija).subscribe((result:any)=>{

    alert("Uspješno ste se registrovali");
    //this.prikaziObavjestenje("Nova registracija", "Uspješno ste registrovali svoj profil");

  this.prijava.korisnickoIme=this.registracija.username;
  this.prijava.lozinka=this.registracija.password;

  this.httpKlijent.post(MyConfig.adresaServera+'/Autentifikacija/Login',this.prijava).subscribe((response : any)=>{

      sessionStorage.setItem("autentifikacija-token", JSON.stringify(response));
      this.router.navigateByUrl("/home-page");


    }
  )

    });}

    else this.prikaziObavjestenje("Neadekvatno ispunjena forma za registraciju", "Molimo ispunite sva obavezna polja ili ispravno unesite lozinku, pa ponovo pokušajte");



  }
  private prikaziObavjestenje(naslov : string, sadrzaj : string) {
    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = naslov;
    this.obavjestenjeSadrzaj = sadrzaj;
  }

  private validirajFormu() {
    var osnovneInformacije : boolean = this.registracija.username != null && this.registracija.username?.length > 0
      && this.registracija.password != null && this.registracija.password?.length > 0
      && this.registracija.ime != null && this.registracija.ime?.length > 0
      && this.registracija.prezime != null && this.registracija.prezime?.length > 0
      && this.registracija.email != null && this.registracija.email?.length > 0;
    var dodatneInformacije : boolean = true;
    if (this.loginInformacije.isPermisijaKorisnik)
      dodatneInformacije = this.registracija.adresaStanovanja != null && this.registracija.adresaStanovanja?.length > 0
        && this.registracija.brojTelefona != null && this.registracija.brojTelefona?.length > 0
        && this.registracija.opstinaId != null;

    return osnovneInformacije && dodatneInformacije;
  }
  provjeriPolje(polje: any) {
    if (polje.invalid && (polje.dirty || polje.touched)){
      if (polje.errors?.['required']){
        return 'Niste popunili ovo polje!';
      }
      else {
        return '';
      }
    }
    return 'Obavezno polje za unos';
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
