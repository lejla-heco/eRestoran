import { Component, OnInit } from '@angular/core';
import {MeniStavka} from "../meni/view-models/meni-stavka-vm";
import {Zaposlenik} from "./view-models/zaposlenik-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-zaposlenik',
  templateUrl: './zaposlenik.component.html',
  styleUrls: ['./zaposlenik.component.css']
})
export class ZaposlenikComponent implements OnInit {
  zaposlenici : Zaposlenik[] = null;
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.ucitajZaposlenike();
  }
  public ucitajZaposlenike() {
    this.httpKlijent.get("https://localhost:44325"+"/Zaposlenik/GetAllPaged").subscribe((result : any)=>{
      this.zaposlenici = result;
    })
  }

}
