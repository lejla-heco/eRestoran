import { Component, OnInit } from '@angular/core';
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {PosebnaPonudaStavka} from "../posebna-ponuda/view-models/posebna-ponuda-stavka-vm";

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  public posebnaPonuda: PosebnaPonudaStavka[] = null;

  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {
    this.getPosebnaPonuda();
  }

  private getPosebnaPonuda() {
    this.httpKlijent.get(MyConfig.adresaServera + "/PosebnaPonuda/GetAll").subscribe((result:any) =>{
      this.posebnaPonuda = result;
    })
  }
}
