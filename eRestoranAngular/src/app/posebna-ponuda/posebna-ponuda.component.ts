import { Component, OnInit } from '@angular/core';
import {PosebnaPonudaStavka} from "./view-models/posebna-ponuda-stavka-vm";
import {HttpClient} from "@angular/common/http";
import { MyConfig } from '../my-config';
import {Router} from "@angular/router";
import {StavkaNarudzbe} from "../narudzba/view-models/stavka-narudzbe-vm";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {LoginInformacije} from "../helper/login-informacije";

@Component({
  selector: 'app-posebna-ponuda',
  templateUrl: './posebna-ponuda.component.html',
  styleUrls: ['./posebna-ponuda.component.css']
})
export class PosebnaPonudaComponent implements OnInit {
  posebnaPonuda : PosebnaPonudaStavka[] = null;
  odabranaStavka: PosebnaPonudaStavka = null;
  loginInformacije : LoginInformacije = null;
  novaStavkaNarudzbe : StavkaNarudzbe = new StavkaNarudzbe();
  closeModal: boolean = false;

  constructor(private httpKlijent : HttpClient, private router : Router) {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.getPosebnaPonuda();
  }

  private getPosebnaPonuda() {
    this.httpKlijent.get(MyConfig.adresaServera + "/PosebnaPonuda/GetAll").subscribe((result:any) =>{
      this.posebnaPonuda = result;
    })
  }

  public prikaziDetalje(stavka : PosebnaPonudaStavka){
    this.closeModal = false;
    this.odabranaStavka = stavka;
  }

  ukloni(id : number) {
    this.httpKlijent.post(MyConfig.adresaServera + "/PosebnaPonuda/Ukloni", id, MyConfig.httpOpcije()).subscribe((result : any)=>{
      this.getPosebnaPonuda();
    });
  }

  createRange(ocjena: number) {
    if (ocjena == null || ocjena == 0) return Array();
    let velicina = Math.round(ocjena);
    return new Array(velicina);
  }

  dodajUKorpu(stavka: PosebnaPonudaStavka) {
    this.novaStavkaNarudzbe.meniStavkaId = stavka.id;
    this.httpKlijent.post(MyConfig.adresaServera+"/Narudzba/AddStavka",this.novaStavkaNarudzbe, MyConfig.httpOpcije()).subscribe((response : any)=>{
      document.getElementById('kolicina').innerHTML = response;
      this.zatvoriModal();
    });
  }

  animiraj() {
    return this.closeModal == true? 'animate__animated animate__fadeOutDownBig' : 'animate__animated animate__fadeInDownBig';
  }

  zatvoriModal(){
    this.closeModal = true;
    this.animiraj();

    setTimeout(()=>{
      this.odabranaStavka = null;
    },1000);
  }
}
