import { Component, OnInit } from '@angular/core';
import {Opstina} from "../registracija/view-models/opstina-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {Poslovnica} from "./view-models/poslovnica-vm";

@Component({
  selector: 'app-poslovnica',
  templateUrl: './poslovnica.component.html',
  styleUrls: ['./poslovnica.component.css']
})
export class PoslovnicaComponent implements OnInit {
  opstine: Opstina[] = null;
  poslovnica : Poslovnica = new Poslovnica();

  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {
    this.getAllOpstine();
  }

  private getAllOpstine() {
    this.httpKlijent.get( MyConfig.adresaServera + "/Opstina/GetAll").subscribe((result:any)=>{
      this.opstine = result;
    });
  }

  posaljiPodatke() {
    this.poslovnica.opstinaId = parseInt(this.poslovnica.opstinaId.toString());
      this.httpKlijent.post(MyConfig.adresaServera + '/Poslovnica/Add', this.poslovnica, MyConfig.httpOpcije()).subscribe((response : any)=>{
        alert("Uspješno dodana nova poslovnica!");
      });
  }
}
