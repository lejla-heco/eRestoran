import { Component, OnInit } from '@angular/core';
import {NovaMeniStavka} from "../../meni/view-models/nova-meni-stavka-vm";
import {NoviZaposlenik} from "../view-models/novi-zaposlenik-vm";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../my-config";

@Component({
  selector: 'app-novi-zaposlenik',
  templateUrl: './novi-zaposlenik.component.html',
  styleUrls: ['./novi-zaposlenik.component.css']
})
export class NoviZaposlenikComponent implements OnInit {
  noviZaposlenik : NoviZaposlenik = new NoviZaposlenik();

  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  fieldText:boolean;//sakrivanje sifre
  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {

  }
  generisiPreview() {
    // @ts-ignore
    var file = document.getElementById("fajl-input").files[0];

    if (file){
      var reader = new FileReader();
      reader.onload = function (){
        document.getElementById("preview-slika").setAttribute("src", reader.result.toString());
      }
      reader.readAsDataURL(file);
    }
  }
  posaljiPodatke() {
    if(this.validirajFormu()){
    // @ts-ignore
    var file = document.getElementById("fajl-input").files[0];
    var data = new FormData();

    data.append("slikaZaposlenika", file);
    this.httpKlijent.post(MyConfig.adresaServera + "/Zaposlenik/Add", this.noviZaposlenik).subscribe((result : any)=>{
      this.httpKlijent.post(MyConfig.adresaServera + "/Zaposlenik/AddSlika/" + result, data).subscribe((result:any)=>{
        this.prikaziObavjestenje("Dodavanje zaposlenika uspješno","Uspješno ste dodali podatke o zaposleniku " + this.noviZaposlenik.ime + " " + this.noviZaposlenik.prezime);
        this.ocistiFormu();
      });
    });}
    else{
      this.prikaziObavjestenje("Neadekvatno ispunjena forma za promjenu ličnih podataka","Molimo ispunite sva obavezna polja, pa pokušajte ponovo.");
    }
  }

  private prikaziObavjestenje(naslov : string, sadrzaj : string) {
    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = naslov;
    this.obavjestenjeSadrzaj = sadrzaj;
  }
  validirajFormu() : boolean{
    // @ts-ignore
    var slika = document.getElementById("fajl-input").files[0];
    return this.noviZaposlenik.username != null && this.noviZaposlenik.username?.length > 0
      && this.noviZaposlenik.password != null && this.noviZaposlenik.password?.length > 0
      && this.noviZaposlenik.ime != null && this.noviZaposlenik.ime?.length > 0
      && this.noviZaposlenik.prezime != null && this.noviZaposlenik.prezime?.length > 0
      && this.noviZaposlenik.email != null && this.noviZaposlenik.email?.length > 0
      && slika!=null

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
  prikaziSakrij() {
    this.fieldText = !this.fieldText;
  }

  ocistiFormu(){
    this.noviZaposlenik.ime = null;
    this.noviZaposlenik.prezime = null;
    this.noviZaposlenik.email = null;
    this.noviZaposlenik.username = null;
    this.noviZaposlenik.password = null;

    document.getElementById("preview-slika").setAttribute("src","");
  }


  animirajObavjestenje() {
    return this.closeModal == true? 'animate__animated animate__bounceOut' : 'animate__animated animate__bounceIn';
  }

  zatvoriModalObavjestenje(){
    this.closeModal = true;
    this.animirajObavjestenje();
    setTimeout(() => {
      this.obavjestenje = false;
    },1000);
  }
}
