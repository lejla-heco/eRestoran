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
    this.novaStavka.meniGrupaId = parseInt(this.novaStavka.meniGrupaId.toString());
    var data = new FormData();
    data.append("slikaMeniStavke", file);
    this.httpKlijent.post(MyConfig.adresaServera + "/Meni/Add", this.novaStavka, MyConfig.httpOpcije()).subscribe((result : any)=>{
      this.httpKlijent.post(MyConfig.adresaServera + "/Meni/AddSlika/" + result, data, MyConfig.httpOpcije()).subscribe((result:any)=>{
        alert("Uspjesno dodana nova stavka");
        this.ocistiFormu();
      });
    });
  }

  ocistiFormu(){
    this.novaStavka.naziv = null;
    this.novaStavka.opis = null;
    this.novaStavka.cijena = null;
    this.novaStavka.snizenaCijena = null;
    this.novaStavka.meniGrupaId = null;
    document.getElementById("preview-slika").setAttribute("src","");
  }

  private validirajFormu() {

  }
}
