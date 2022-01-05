import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {NovaMeniStavka} from "../view-models/nova-meni-stavka-vm";
import {MyConfig} from "../../my-config";
import {MeniGrupa} from "../view-models/meni-grupa-vm";

@Component({
  selector: 'app-nova-stavka',
  templateUrl: './nova-stavka.component.html',
  styleUrls: ['./nova-stavka.component.css']
})
export class NovaStavkaComponent implements OnInit {
  novaStavka : NovaMeniStavka = new NovaMeniStavka();
  meniGrupe : MeniGrupa[] = null;

  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {
    this.getAllMeniGrupe();
    this.novaStavka.meniGrupaId = -1;
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
    if (this.validirajFormu()) {
      // @ts-ignore
      var file = document.getElementById("fajl-input").files[0];
      this.novaStavka.meniGrupaId = parseInt(this.novaStavka.meniGrupaId.toString());
      var data = new FormData();
      data.append("slikaMeniStavke", file);
      if (file.size <= 250 * 1000) {

        this.httpKlijent.post(MyConfig.adresaServera + "/Meni/Add", this.novaStavka, MyConfig.httpOpcije()).subscribe((result: any) => {
          this.httpKlijent.post(MyConfig.adresaServera + "/Meni/AddSlika/" + result, data, MyConfig.httpOpcije()).subscribe((response: any) => {
            this.prikaziObavjestenje("Dodana nova stavka", "Uspješno ste dodali novu stavku na meni");
            this.ocistiFormu();
          });
        });
      }
      else this.prikaziObavjestenje("Prevelika fotografija", "Možete učitati fotografije do 250 KB, učitajte manju fotografiju");
    }
    else {
      this.prikaziObavjestenje("Neadekvatno ispunjena forma za dodavanje nove meni stavke", "Molimo ispunite sva obavezna polja, pa ponovo pokušajte");
    }
  }

  ocistiFormu(){
    this.novaStavka.naziv = null;
    this.novaStavka.opis = null;
    this.novaStavka.cijena = null;
    this.novaStavka.snizenaCijena = null;
    this.novaStavka.meniGrupaId = -1;
    document.getElementById("preview-slika").setAttribute("src","");
  }

  animirajObavjestenje() {
    return this.closeModal == true? 'animate__animated animate__bounceOut' : 'animate__animated animate__bounceIn';
  }

  zatvoriModalObavjestenje(){
    this.closeModal = true;
    this.animirajObavjestenje();
    setTimeout(() => {
      this.obavjestenje = false;
    },500);
  }

  validirajFormu() : boolean{
    // @ts-ignore
    var slika = document.getElementById("fajl-input").files[0];
    return this.novaStavka.naziv != null && this.novaStavka.naziv?.length > 0
      && this.novaStavka.opis != null && this.novaStavka.opis?.length > 0
      && this.novaStavka.cijena != null && this.novaStavka.snizenaCijena != null
      && this.novaStavka.meniGrupaId != -1 && slika != null;
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
}
