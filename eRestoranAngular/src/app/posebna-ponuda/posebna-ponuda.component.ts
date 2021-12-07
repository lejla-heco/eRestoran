import { Component, OnInit } from '@angular/core';
import {PosebnaPonudaStavka} from "./view-models/posebna-ponuda-stavka-vm";
import {HttpClient} from "@angular/common/http";
import { MyConfig } from '../my-config';

@Component({
  selector: 'app-posebna-ponuda',
  templateUrl: './posebna-ponuda.component.html',
  styleUrls: ['./posebna-ponuda.component.css']
})
export class PosebnaPonudaComponent implements OnInit {
  posebnaPonuda : PosebnaPonudaStavka[] = null;
  stavkaDetalji : PosebnaPonudaStavka = null;
  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {
    this.getPosebnaPonuda();
  }

  private getPosebnaPonuda() {
    this.httpKlijent.get(MyConfig.adresaServera + "/PosebnaPonuda/GetAll").subscribe((result:any) =>{
      this.posebnaPonuda = result;
    })
  }

  public prikaziDetalje(stavka : PosebnaPonudaStavka){
    this.stavkaDetalji = stavka;
  }

  ukloni(id : number) {
    this.httpKlijent.post(MyConfig.adresaServera + "/PosebnaPonuda/Ukloni", id).subscribe((result : any)=>{
      alert("Uklonjena stavka posebne ponude");
      this.getPosebnaPonuda();
    });
  }
}
