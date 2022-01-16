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

  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";
  fieldText:boolean;
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
    this.httpKlijent.get(MyConfig.adresaServera+ "/Zaposlenik/GetById/"+this.id).subscribe((result : any) =>{
      this.urediZaposlenik = result;
    })
  }

  posaljiPodatke() {
    if(this.validirajFormu()){
    // @ts-ignore
    var file = document.getElementById("fajl-input").files[0];

    var data = new FormData();
    data.append("slikaZaposlenika", file);
    this.httpKlijent.post(MyConfig.adresaServera + "/Zaposlenik/Update/"+ this.id, this.urediZaposlenik).subscribe((result :any)=>{
      this.httpKlijent.post(MyConfig.adresaServera + "/Zaposlenik/AddSlika/" + result, data).subscribe((result: any)=>{
        this.prikaziObavjestenje("Ažuriranje podataka uspješno","Uspješno ste uredili podatke o zaposleniku " + this.urediZaposlenik.ime + " " + this.urediZaposlenik.prezime);
      });
    });}
    else{
      this.prikaziObavjestenje("Neadekvatno ispunjena forma za promjenu ličnih podataka","Molimo ispunite sva obavezna polja, pa pokušajte ponovo.");
    }
  }
  private prikaziObavjestenje(naslov : string, sadrzaj : string) {
    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = naslov;
    this.obavjestenjeSadrzaj = sadrzaj;
  }
  validirajFormu() : boolean{
    // @ts-ignore
    //var slika = document.getElementById("fajl-input").files[0];
    return this.urediZaposlenik.korisnickoIme != null && this.urediZaposlenik.korisnickoIme?.length > 0
      && this.urediZaposlenik.lozinka != null && this.urediZaposlenik.lozinka?.length > 0
      && this.urediZaposlenik.ime != null && this.urediZaposlenik.ime?.length > 0
      && this.urediZaposlenik.prezime != null && this.urediZaposlenik.prezime?.length > 0
      && this.urediZaposlenik.email != null && this.urediZaposlenik.email?.length > 0
      && this.urediZaposlenik.slika != null

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
  prikaziSakrij() {
    this.fieldText = !this.fieldText;
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
