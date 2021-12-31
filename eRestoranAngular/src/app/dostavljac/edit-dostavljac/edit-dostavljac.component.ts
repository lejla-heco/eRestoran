import { Component, OnInit } from '@angular/core';
import {EditZaposlenik} from "../../zaposlenik/view-models/edit-zaposlenik-vm";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {MyConfig} from "../../my-config";
import {EditDostavljac} from "../view-models/edit-dostavljac-vm";

@Component({
  selector: 'app-edit-dostavljac',
  templateUrl: './edit-dostavljac.component.html',
  styleUrls: ['./edit-dostavljac.component.css']
})
export class EditDostavljacComponent implements OnInit {

  id : number;
  urediDostavljac : EditDostavljac =null;
  constructor( private httpKlijent :HttpClient ,private route: ActivatedRoute,private router:Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = +params['id'];
    });
    this.dohvatiDostavljaca();

  }
  private dohvatiDostavljaca() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Dostavljac/GetById/"+this.id).subscribe((result : any) =>{
      this.urediDostavljac = result;
    })
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

  //MyConfig.adresaServera
    posaljiPodatke() {
  // @ts-ignore
  var file = document.getElementById("fajl-input").files[0];

  var data = new FormData();
  data.append("slikaZaposlenika", file);
  this.httpKlijent.post(MyConfig.adresaServera + "/Dostavljac/Update/"+ this.id, this.urediDostavljac).subscribe((result :any)=>{
  this.httpKlijent.post(MyConfig.adresaServera + "/Dostavljac/AddSlika/" + result, data).subscribe((result: any)=>{
  alert("Uspješno uređen dostavljač "+ this.urediDostavljac.ime+" "+this.urediDostavljac.prezime);
  this.router.navigate(['/dostavljac']);
});
});

}
}
