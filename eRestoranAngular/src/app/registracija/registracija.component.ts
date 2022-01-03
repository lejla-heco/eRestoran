import { Component, OnInit } from '@angular/core';
import {Login} from "../login/view-models/login-vm";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Registracija} from "./view-models/registracija-vm";
import {MeniGrupa} from "../meni/view-models/meni-grupa-vm";
import {MyConfig} from "../my-config";
import {Opstina} from "./view-models/opstina-vm";

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

  constructor(private httpKlijent : HttpClient, private router : Router) { }



  ngOnInit(): void {
    this.getAllOpstine();

  }
  private getAllOpstine() {
    this.httpKlijent.get( MyConfig.adresaServera + "/Opstina/GetAll").subscribe((result:any)=>{
      this.opstine = result;
    });
  }

  registracijaPodataka() {
    if(this.registracija.password==this.sifra){

    this.registracija.opstinaId = parseInt(this.registracija.opstinaId.toString());
  this.httpKlijent.post(MyConfig.adresaServera + "/Korisnik/Add",this.registracija).subscribe((result:any)=>{
   /* this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = "Nova registracija";
    this.obavjestenjeSadrzaj = "Uspješno ste registrovali svoj profil";*/
    alert("Uspješno ste se registrovali");

  this.prijava.korisnickoIme=this.registracija.username;
  this.prijava.lozinka=this.registracija.password;

  this.httpKlijent.post(MyConfig.adresaServera+'/Autentifikacija/Login',this.prijava).subscribe((response : any)=>{

      sessionStorage.setItem("autentifikacija-token", JSON.stringify(response));
      this.router.navigateByUrl("/home-page");


    }
  )

    });}
    else //alert("Neispravna lozinka");
    {
      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov = "Greška";
      this.obavjestenjeSadrzaj = "Neispravna lozinka";
    }


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
