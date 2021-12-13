import { Component, OnInit } from '@angular/core';
import {NovaMeniStavka} from "../../meni/view-models/nova-meni-stavka-vm";
import {NoviZaposlenik} from "../view-models/novi-zaposlenik-vm";

@Component({
  selector: 'app-novi-zaposlenik',
  templateUrl: './novi-zaposlenik.component.html',
  styleUrls: ['./novi-zaposlenik.component.css']
})
export class NoviZaposlenikComponent implements OnInit {
  noviZaposlenik : NoviZaposlenik = new NoviZaposlenik();
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

}
