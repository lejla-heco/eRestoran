import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {NovaMeniStavka} from "../view-models/nova-meni-stavka-vm";
import {MyConfig} from "../../my-config";
import {MeniGrupa} from "../view-models/meni-grupa-vm";

@Component({
  selector: 'app-nova-stavka',
  templateUrl: './nova-stavka.component.html',
  styleUrls: ['./nova-stavka.component.css']
})
export class NovaStavkaComponent implements OnInit {
  novaStavka : NovaMeniStavka = new NovaMeniStavka();
  meniGrupe : MeniGrupa[] = null;
  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {
    this.getAllMeniGrupe();
  }

  private getAllMeniGrupe() {
    this.httpKlijent.get(MyConfig.adresaServera + "/MeniGrupa/GetAll").subscribe((result:any)=>{
      this.meniGrupe = result;
    });
  }
}
