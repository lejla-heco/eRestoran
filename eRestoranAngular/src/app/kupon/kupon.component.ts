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
  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {
  }

  posaljiPodatke() {
    this.httpKlijent.post(MyConfig.adresaServera + "/Kupon/GenerisiKupon", this.popusniKupon, MyConfig.httpOpcije()).subscribe((response : any) => {
      alert("Uspješno generisan kupon " + response.kod);
      this.ocistiFormu();
    });
  }

  private ocistiFormu() {
    this.popusniKupon.popust = null;
    this.popusniKupon.maksimalniBrojKorisnika = null;
  }
}
