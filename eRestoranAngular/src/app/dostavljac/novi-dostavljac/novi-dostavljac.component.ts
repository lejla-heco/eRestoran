import { Component, OnInit } from '@angular/core';
import {NoviDostavljac} from "../view-models/novi-dostavljac-vm";
import {MyConfig} from "../../my-config";

@Component({
  selector: 'app-novi-dostavljac',
  templateUrl: './novi-dostavljac.component.html',
  styleUrls: ['./novi-dostavljac.component.css']
})
export class NoviDostavljacComponent implements OnInit {
noviDostavljac:NoviDostavljac= new NoviDostavljac();
  constructor() { }

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


  }

}
