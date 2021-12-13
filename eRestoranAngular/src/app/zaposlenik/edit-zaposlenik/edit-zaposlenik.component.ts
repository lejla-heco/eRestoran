import { Component, OnInit } from '@angular/core';
import {EditMeniStavka} from "../../meni/view-models/edit-meni-stavka-vm";
import {EditZaposlenik} from "../view-models/edit-zaposlenik-vm";
import {ActivatedRoute, Router} from "@angular/router";
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
  constructor( private httpKlijent :HttpClient ,private route: ActivatedRoute,private router:Router) { }

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
    this.httpKlijent.get(MyConfig.adresaServera + "/Zaposlenik/GetById/"+this.id).subscribe((result : any) =>{
      this.urediZaposlenik = result;
    })
  }

  posaljiPodatke() {
    // @ts-ignore
    var file = document.getElementById("fajl-input").files[0];

    var data = new FormData();
    data.append("slikaZaposlenika", file);
    this.httpKlijent.post(MyConfig.adresaServera + "/Zaposlenik/Update/"+ this.id, this.urediZaposlenik).subscribe((result :any)=>{
      this.httpKlijent.post(MyConfig.adresaServera + "/Zaposlenik/AddSlika/" + result, data).subscribe((result: any)=>{
        alert("Uspješno uređen zaposlenik "+ this.urediZaposlenik.ime+" "+this.urediZaposlenik.prezime);
        this.router.navigate(['/zaposlenik']);
      });
    });
  }
}
