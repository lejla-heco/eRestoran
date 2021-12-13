import { Component, OnInit } from '@angular/core';
import {EditMeniStavka} from "../../meni/view-models/edit-meni-stavka-vm";
import {EditZaposlenik} from "../view-models/edit-zaposlenik-vm";
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../my-config";

@Component({
  selector: 'app-edit-zaposlenik',
  templateUrl: './edit-zaposlenik.component.html',
  styleUrls: ['./edit-zaposlenik.component.css']
})
export class EditZaposlenikComponent implements OnInit {
  id : number;
  urediZaposlenik : EditZaposlenik =null;
  constructor( private httpKlijent :HttpClient ,private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = +params['id'];
    });
    this.dohvatiZaposlenika();
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
  private dohvatiZaposlenika() {
    this.httpKlijent.get("https://localhost:44325" + "/Zaposlenik/GetById/"+this.id).subscribe((result : any) =>{
      this.urediZaposlenik = result;
    })
  }

}
