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
    this.validirajFormu();
    // @ts-ignore
    var file = document.getElementById("fajl-input").files[0];
    var data = new FormData();

    data.append("slikaZaposlenika", file);
    this.httpKlijent.post("https://localhost:44325" + "/Zaposlenik/Add", this.noviZaposlenik).subscribe((result : any)=>{
      this.httpKlijent.post("https://localhost:44325" + "/Zaposlenik/AddSlika/" + result, data).subscribe((result:any)=>{
        alert("Uspjesno dodan novi zaposlenik");
        this.ocistiFormu();
      });
    });
  }


  ocistiFormu(){
    this.noviZaposlenik.ime = null;
    this.noviZaposlenik.prezime = null;
    this.noviZaposlenik.email = null;
    this.noviZaposlenik.username = null;
    this.noviZaposlenik.password = null;

    document.getElementById("preview-slika").setAttribute("src","");
  }

  private validirajFormu() {

  }
}
