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

  constructor(private httpKlijent : HttpClient, private router : Router) { }



  ngOnInit(): void {
    this.getAllMeniGrupe();

  }
  private getAllMeniGrupe() {
    this.httpKlijent.get( "https://localhost:44325"+ "/Opstina/GetAll").subscribe((result:any)=>{
      this.opstine = result;
    });
    //MyConfig.adresaServera
  }

}
