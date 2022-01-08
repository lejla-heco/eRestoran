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


  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";
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


    posaljiPodatke() {
  // @ts-ignore
  var file = document.getElementById("fajl-input").files[0];

  var data = new FormData();
  data.append("slikaDostavljaca", file);
  this.httpKlijent.post(MyConfig.adresaServera + "/Dostavljac/Update/"+ this.id, this.urediDostavljac).subscribe((result :any)=>{
  this.httpKlijent.post(MyConfig.adresaServera + "/Dostavljac/AddSlika/" + result, data).subscribe((result: any)=>{
    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = "Ažuriranje podataka uspješno";
    this.obavjestenjeSadrzaj = "Uspješno ste uredili podatke o dostavljaču "+ this.urediDostavljac.ime+" "+this.urediDostavljac.prezime;
 // this.router.navigate(['/dostavljac']);
});
});

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
