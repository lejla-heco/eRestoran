import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {NovaMeniStavka} from "../view-models/nova-meni-stavka-vm";
import {MeniGrupa} from "../view-models/meni-grupa-vm";
import {MyConfig} from "../../my-config";
import {ActivatedRoute} from "@angular/router";
import {MeniStavka} from "../view-models/meni-stavka-vm";

@Component({
  selector: 'app-edit-stavka',
  templateUrl: './edit-stavka.component.html',
  styleUrls: ['./edit-stavka.component.css']
})
export class EditStavkaComponent implements OnInit {
 @Input() urediStavka : any;
  meniGrupe : MeniGrupa[] = null;
  vraceniId : number = null;
  constructor(private httpKlijent : HttpClient,private route: ActivatedRoute) { }

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

  /*  this.httpKlijent.post(MojConfig.adresa_servera+ "/Student/Update/"+this.urediStudent.id,this.urediStudent).subscribe
    ((x:any)=>{
      //alert("uredu..."+ x.opstina_rodjenja.drzava.naziv);
      alert("Vaši podaci su uspješno izmjenjeni, "+x.ime+" "+x.prezime);
      this.urediStudent=null;// kako ucitati kod  dodan u html???
    });*/

    /*var file = document.getElementById("fajl-input").files[0];
    this.urediStavka.meniGrupaId = parseInt(this.urediStavka.meniGrupaId.toString());
    var data = new FormData();
    data.append("slikaMeniStavke", file);*/
    this.httpKlijent.post(MyConfig.adresaServera + "/Meni/Update/"+ this.urediStavka, this.urediStavka).subscribe((result : any)=>{
      //this.httpKlijent.post(MyConfig.adresaServera + "/Meni/AddSlika/" + result, data).subscribe((result:any)=>{
        alert("uspjesno?");
      //});
    });
  }

}
