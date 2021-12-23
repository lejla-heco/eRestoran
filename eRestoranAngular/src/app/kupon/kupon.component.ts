import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Kupon} from "./view-models/kupon";
import {MyConfig} from "../my-config";

@Component({
  selector: 'app-kupon',
  templateUrl: './kupon.component.html',
  styleUrls: ['./kupon.component.css']
})
export class KuponComponent implements OnInit {
  popusniKupon : Kupon = new Kupon();
  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {
  }

  posaljiPodatke() {
    this.httpKlijent.post(MyConfig.adresaServera + "/Kupon/GenerisiKupon", this.popusniKupon, MyConfig.httpOpcije()).subscribe((response : any) => {
      this.ocistiFormu();
      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov = "Novi kupon za Vaše korisnike";
      this.obavjestenjeSadrzaj = "Uspješno ste generisali novi kupon sa kodom: " + response.kod;
    });
  }

  private ocistiFormu() {
    this.popusniKupon.popust = null;
    this.popusniKupon.maksimalniBrojKorisnika = null;
  }

  animirajObavjestenje() {
    return this.closeModal == true? 'animate__animated animate__bounceOut' : 'animate__animated animate__bounceIn';
  }

  zatvoriModalObavjestenje(){
    this.closeModal = true;
    this.animirajObavjestenje();
    this.obavjestenje = setTimeout(function (){
      return false;
    },1000)== 0? false : true;
  }

}
