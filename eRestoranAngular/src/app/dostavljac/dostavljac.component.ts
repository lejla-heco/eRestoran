import { Component, OnInit } from '@angular/core';
import {Zaposlenik} from "../zaposlenik/view-models/zaposlenik-vm";
import {Dostavljac} from "./view-models/dostavljac-vm";

@Component({
  selector: 'app-dostavljac',
  templateUrl: './dostavljac.component.html',
  styleUrls: ['./dostavljac.component.css']
})
export class DostavljacComponent implements OnInit {
  dostavljaci : Dostavljac[] = null;
  odabraniDostavljac: Dostavljac = null;//brisanje
  obrisan:boolean=false;
  id : number = null;//edit
  constructor() { }

  ngOnInit(): void {
  }

}
