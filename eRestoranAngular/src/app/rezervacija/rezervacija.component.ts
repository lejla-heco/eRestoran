import { Component, OnInit } from '@angular/core';
import {Prigoda} from "./view-models/prigoda-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {NovaRezervacija} from "./view-models/nova-rezervacija-vm";

@Component({
  selector: 'app-rezervacija',
  templateUrl: './rezervacija.component.html',
  styleUrls: ['./rezervacija.component.css']
})
export class RezervacijaComponent implements OnInit {
  public prigode : Prigoda[] = null;
  rezervacija:NovaRezervacija= new NovaRezervacija();


  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.getAllPrigode();
  }

  private getAllPrigode() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Prigoda/GetAll", MyConfig.httpOpcije()).subscribe((result:any)=>{
      this.prigode = result;

    });
  }

  posaljiPodatke() {
    this.httpKlijent.post(MyConfig.adresaServera + "/Rezervacija/Add",this.rezervacija, MyConfig.httpOpcije()).subscribe((result:any)=>{
      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov = "Nova rezervacija";
      this.obavjestenjeSadrzaj = "Uspješno ste dodali novu rezervaciju";

    });
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
}
