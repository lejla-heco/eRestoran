import { Component, OnInit } from '@angular/core';
import {MeniGrupa} from "../meni/view-models/meni-grupa-vm";
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
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.getAllPrigode();
  }

  private getAllPrigode() {
    this.httpKlijent.get("https://localhost:44325/Prigoda/GetAll", MyConfig.httpOpcije()).subscribe((result:any)=>{
      this.prigode = result;
    });
  }

  posaljiPodatke() {
    this.httpKlijent.post("https://localhost:44325/Rezervacija/Add",this.rezervacija, MyConfig.httpOpcije()).subscribe((result:any)=>{
      alert("Uspješno dodana nova rezervacija");
    });
  }
}
