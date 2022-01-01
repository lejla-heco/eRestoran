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
    if (this.validirajFormu()) {
      this.httpKlijent.post(MyConfig.adresaServera + "/Kupon/GenerisiKupon", this.popusniKupon, MyConfig.httpOpcije()).subscribe((response: any) => {
        this.ocistiFormu();
        this.prikaziObavjestenje("Novi kupon za Vaše korisnike", "Uspješno ste generisali novi kupon sa kodom: " + response.kod);
      });
    }
    else this.prikaziObavjestenje("Neadekvatno ispunjena forma za generisanje kupona", "Molimo ispunite sva obavezna polja, pa ponovo pokušajte");
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
    },500)== 0? false : true;
  }

  provjeriPolje(polje: any) {
    if (polje.invalid && (polje.dirty || polje.touched)){
      if (polje.errors?.['required']){
        return 'Niste popunili ovo polje!';
      }
      else {
        return '';
      }
    }
    return 'Obavezno polje za unos';
  }

  private validirajFormu() {
    return this.popusniKupon.popust != null && this.popusniKupon.maksimalniBrojKorisnika != null;
  }

  private prikaziObavjestenje(naslov : string, sadrzaj : string) {
    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = naslov;
    this.obavjestenjeSadrzaj = sadrzaj;
  }

}
