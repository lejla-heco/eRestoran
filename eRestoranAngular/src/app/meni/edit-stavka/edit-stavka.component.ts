import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MeniGrupa} from "../view-models/meni-grupa-vm";
import {MyConfig} from "../../my-config";
import {ActivatedRoute} from "@angular/router";
import {EditMeniStavka} from "../view-models/edit-meni-stavka-vm";

@Component({
  selector: 'app-edit-stavka',
  templateUrl: './edit-stavka.component.html',
  styleUrls: ['./edit-stavka.component.css']
})
export class EditStavkaComponent implements OnInit {
  id : number;
  urediStavka : EditMeniStavka = null;
  meniGrupe : MeniGrupa[] = null;

  constructor(private httpKlijent : HttpClient, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = +params['id'];
    });
    this.dohvatiMeniStavku();
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

     // @ts-ignore
    var file = document.getElementById("fajl-input").files[0];
    this.urediStavka.meniGrupaId = parseInt(this.urediStavka.meniGrupaId.toString());
    var data = new FormData();
    data.append("slikaMeniStavke", file);
    this.httpKlijent.post(MyConfig.adresaServera + "/Meni/Update/"+ this.urediStavka.id, this.urediStavka).subscribe((result :any)=>{
      this.httpKlijent.post(MyConfig.adresaServera + "/Meni/AddSlika/" + result, data).subscribe((result: any)=>{
        alert("Uspješno uređena stavka menija "+ this.urediStavka.naziv);
      });
    });
  }

  private dohvatiMeniStavku() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Meni/GetById/"+this.id).subscribe((result : any) =>{
      this.urediStavka = result;
    })
  }
}
