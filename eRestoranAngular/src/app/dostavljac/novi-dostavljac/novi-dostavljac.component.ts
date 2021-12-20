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


    // @ts-ignore
    var file = document.getElementById("fajl-input").files[0];
    var data = new FormData();

    data.append("slikaDostavljaca", file);
    this.httpKlijent.post("https://localhost:44325" + "/Dostavljac/Add", this.noviDostavljac).subscribe((result : any)=>{
      this.httpKlijent.post("https://localhost:44325" + "/Dostavljac/AddSlika/" + result, data).subscribe((result:any)=>{
        alert("Uspjesno dodan novi dostavljač");
        this.ocistiFormu();
      });
    });

  }
  ocistiFormu(){
    this.noviDostavljac.ime = null;
    this.noviDostavljac.prezime = null;
    this.noviDostavljac.email = null;
    this.noviDostavljac.username = null;
    this.noviDostavljac.password = null;

    document.getElementById("preview-slika").setAttribute("src","");
  }

}
