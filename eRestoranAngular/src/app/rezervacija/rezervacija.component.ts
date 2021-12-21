import { Component, OnInit } from '@angular/core';
import {MeniGrupa} from "../meni/view-models/meni-grupa-vm";
import {Prigoda} from "./view-models/prigoda-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-rezervacija',
  templateUrl: './rezervacija.component.html',
  styleUrls: ['./rezervacija.component.css']
})
export class RezervacijaComponent implements OnInit {
  prigode : Prigoda[] = null;
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.getAllPrigode();
  }

  private getAllPrigode() {
    this.httpKlijent.get("https://localhost:44325/Prigoda/GetAll").subscribe((result:any)=>{
      this.prigode = result;
    });
  }
}
