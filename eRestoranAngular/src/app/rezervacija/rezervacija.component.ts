import { Component, OnInit } from '@angular/core';
import {Prigoda} from "./view-models/prigoda-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {NovaRezervacija} from "./view-models/nova-rezervacija-vm";

@Component({
  selector: 'app-rezervacija',
  templateUrl: './rezervacija.component.html',
  styleUrls: ['./rezervacija.component.css']
})
export class RezervacijaComponent implements OnInit {
  public prigode : Prigoda[] = null;
  rezervacija:NovaRezervacija= new NovaRezervacija();


  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.getAllPrigode();
    this.rezervacija.prigodaID = -1;
  }

  private getAllPrigode() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Prigoda/GetAll", MyConfig.httpOpcije()).subscribe((result:any)=>{
      this.prigode = result;

    });
  }

  posaljiPodatke() {
    if(this.validirajFormu()) {
      this.httpKlijent.post(MyConfig.adresaServera + "/Rezervacija/Add", this.rezervacija, MyConfig.httpOpcije()).subscribe((result: any) => {

        this.prikaziObavjestenje("Nova rezervacija", "Uspješno ste dodali novu rezervaciju");
        this.ocistiFormu();

      });
    }
    else {
      this.prikaziObavjestenje("Neadekvatno ispunjena forma za dodavanje nove meni stavke", "Molimo ispunite sva obavezna polja, pa ponovo pokušajte");
    }

  }
  ocistiFormu(){
    this.rezervacija.brojOsoba = null;
    this.rezervacija.brojStolova = null;
    this.rezervacija.poruka = null;
    this.rezervacija.prigodaID = -1;
    this.rezervacija.datumRezerviranja = null;

  }
  validirajFormu() : boolean{


    return this.rezervacija.brojOsoba != null && this.rezervacija.brojStolova!=null
      && this.rezervacija.datumRezerviranja != null && this.rezervacija.datumRezerviranja?.length > 0
      && this.rezervacija.prigodaID != -1 ;
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
