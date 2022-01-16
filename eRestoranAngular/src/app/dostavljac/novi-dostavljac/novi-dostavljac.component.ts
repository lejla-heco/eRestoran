import { Component, OnInit } from '@angular/core';
import {NoviDostavljac} from "../view-models/novi-dostavljac-vm";
import {MyConfig} from "../../my-config";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-novi-dostavljac',
  templateUrl: './novi-dostavljac.component.html',
  styleUrls: ['./novi-dostavljac.component.css']
})
export class NoviDostavljacComponent implements OnInit {
noviDostavljac:NoviDostavljac= new NoviDostavljac();

  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  fieldText: boolean;// za sakrivanje passworda
  constructor(private httpKlijent: HttpClient) { }

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

    data.append("slikaDostavljaca", file);
    this.httpKlijent.post(MyConfig.adresaServera + "/Dostavljac/Add", this.noviDostavljac).subscribe((result : any)=>{
      this.httpKlijent.post(MyConfig.adresaServera + "/Dostavljac/AddSlika/" + result, data).subscribe((result:any)=>{
        this.obavjestenje = true;
        this.closeModal = false;
        this.obavjestenjeNaslov = "Dodavanje podataka uspješno";
        this.obavjestenjeSadrzaj = "Uspješno ste dodali dostavljača "+ this.noviDostavljac.ime+" "+this.noviDostavljac.prezime;
        this.ocistiFormu();
      });
    });
}
else{
  this.obavjestenje = true;
  this.closeModal = false;
  this.obavjestenjeNaslov = "Neadekvatno ispunjena forma za promjenu ličnih podataka";
  this.obavjestenjeSadrzaj = "Molimo ispunite sva obavezna polja, pa ponovo pokušajte";
}

  }

  /*private validirajFormu() {
    var osnovneInformacije : boolean = this.noviDostavljac.username != null && this.noviDostavljac.username?.length > 0
      && this.noviDostavljac.password != null && this.noviDostavljac.password?.length > 0
      && this.noviDostavljac.ime != null && this.noviDostavljac.ime?.length > 0
      && this.noviDostavljac.prezime != null && this.noviDostavljac.prezime?.length > 0
      && this.noviDostavljac.email != null && this.noviDostavljac.email?.length > 0;
    var dodatneInformacije : boolean = true;
      dodatneInformacije = document.getElementById('preview-slika').getAttribute("src").length > 0;
    return osnovneInformacije && dodatneInformacije;
  }*/

  validirajFormu() : boolean{
    // @ts-ignore
    var slika = document.getElementById("fajl-input").files[0];
    return this.noviDostavljac.username != null && this.noviDostavljac.username?.length > 0
      && this.noviDostavljac.password != null && this.noviDostavljac.password?.length > 0
      && this.noviDostavljac.ime != null && this.noviDostavljac.ime?.length > 0
      && this.noviDostavljac.prezime != null && this.noviDostavljac.prezime?.length > 0
      && this.noviDostavljac.email != null && this.noviDostavljac.email?.length > 0
      && slika != null

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
    this.noviDostavljac.ime = null;
    this.noviDostavljac.prezime = null;
    this.noviDostavljac.email = null;
    this.noviDostavljac.username = null;
    this.noviDostavljac.password = null;

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
