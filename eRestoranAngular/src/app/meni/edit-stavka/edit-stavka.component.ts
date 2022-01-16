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
  urediStavka : EditMeniStavka =null;
  meniGrupe : MeniGrupa[] = null;

  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

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
if(this.validirajFormu()){
     // @ts-ignore
    var file = document.getElementById("fajl-input").files[0];
    this.urediStavka.meniGrupaId = parseInt(this.urediStavka.meniGrupaId.toString());
console.log(this.urediStavka.naziv);
    var data = new FormData();
    data.append("slikaMeniStavke", file);

    this.httpKlijent.post(MyConfig.adresaServera + "/Meni/Update/"+ this.id, this.urediStavka, MyConfig.httpOpcije()).subscribe((result :any)=>{
      this.httpKlijent.post(MyConfig.adresaServera + "/Meni/AddSlika/" + result, data, MyConfig.httpOpcije()).subscribe((result: any)=>{
        this.prikaziObavjestenje("Uređena stavka menija", "Uspješno ste uredili stavku menija: " + this.urediStavka.naziv);

      });
    });}

else {
  this.prikaziObavjestenje("Neadekvatno ispunjena forma za dodavanje nove meni stavke", "Molimo ispunite sva obavezna polja, pa ponovo pokušajte");
}
  }
  validirajFormu() : boolean{
    // @ts-ignore
    //var slika = document.getElementById("fajl-input").files[0];
    return this.urediStavka.naziv != null && this.urediStavka.naziv?.length > 0
      && this.urediStavka.opis != null && this.urediStavka.opis?.length > 0
      && this.urediStavka.cijena != null && this.urediStavka.snizenaCijena != null
      && this.urediStavka.meniGrupaId != -1 && this.urediStavka.slika != null;
  }

  provjeriPolje(polje: any) {
    if (polje.invalid && (polje.dirty || polje.touched)){
      if (polje.errors?.['required']){
        return 'Niste popunili ovo polje!';
      }
      else {
        return '';
      }
    }
    return 'Obavezno polje za unos';
  }

  private prikaziObavjestenje(naslov : string, sadrzaj : string) {
    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = naslov;
    this.obavjestenjeSadrzaj = sadrzaj;
  }
  private dohvatiMeniStavku() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Meni/GetById/"+this.id).subscribe((result : any) =>{
      this.urediStavka = result;
    })
  }

  animirajObavjestenje() {
    return this.closeModal == true? 'animate__animated animate__bounceOut' : 'animate__animated animate__bounceIn';
  }

  zatvoriModalObavjestenje(){
    this.closeModal = true;
    this.animirajObavjestenje();
    this.obavjestenje = setTimeout(function (){
      return false;
    },1000)== 0? false : true;
  }
}
