import { Component, OnInit } from '@angular/core';
import {Poslovnica} from "../home-page/view-models/poslovnica-vm";
import {Opstina} from "../registracija/view-models/opstina-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-poslovnica',
  templateUrl: './poslovnica.component.html',
  styleUrls: ['./poslovnica.component.css']
})
export class PoslovnicaComponent implements OnInit {
  poslovnica : Poslovnica = new Poslovnica();
  opstine: Opstina[] = null;
  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";
  validacija: boolean = false;

  constructor(private httpKlijent : HttpClient, private router : Router) { }

  ngOnInit(): void {
    this.inicijalizirajGoogleMapu();
    this.ucitajOpstine();
    this.poslovnica.opstinaId = -1;
  }

  inicijalizirajGoogleMapu() {

    const map = new google.maps.Map(document.getElementById("map")!, {
      zoom: 17,
      center: { lat: 43.8559, lng: 18.40725 }, //koordinate Sarajeva
    });

    // Novi info window
    let infoWindow = new google.maps.InfoWindow({
      content: "Kliknite na lokaciju da učitate geografsku širinu i dužinu!",
      position: { lat: 43.8559, lng: 18.40725 },
    });

    infoWindow.open(map);

    // click listener
    map.addListener("click", (mapsMouseEvent : any) => {
      // zatvori trenutni info window
      infoWindow.close();

      // napravi novi info window
      infoWindow = new google.maps.InfoWindow({
        position: mapsMouseEvent.latLng,
      });
      infoWindow.setContent(
        JSON.stringify(mapsMouseEvent.latLng.toJSON(), null, 2)
      );
      this.poslovnica.lat = mapsMouseEvent.latLng.toJSON().lat;
      this.poslovnica.lng = mapsMouseEvent.latLng.toJSON().lng;
      infoWindow.open(map);
    });
  }

  private ucitajOpstine() {
    this.httpKlijent.get( MyConfig.adresaServera + "/Opstina/GetAll").subscribe((result:any)=>{
      this.opstine = result;
    });
  }

  dodajPoslovnicu() {
    if (this.validirajFormu()) {
      this.httpKlijent.post(MyConfig.adresaServera + "/Poslovnica/Add", this.poslovnica, MyConfig.httpOpcije()).subscribe((response: any) => {
        this.validacija = false;
        this.ocistiFormu();
        this.prikaziObavjestenje("Dodana nova poslovnica", "Uspješno ste dodali novu poslovnicu na adresi: " + response.adresa)
      });
    }
    else{
      this.validacija = true;
      this.prikaziObavjestenje("Neadekvatno ispunjena forma za dodavanje nove poslovnice", "Molimo ispunite sva obavezna polja, pa ponovo pokušajte");
    }
  }

  ocistiFormu(){
    this.poslovnica.adresa = null;
    this.poslovnica.brojTelefona = null;
    this.poslovnica.radnoVrijemeRedovno = null;
    this.poslovnica.radnoVrijemeVikend = null;
    this.poslovnica.lat = null;
    this.poslovnica.lng = null;
    this.poslovnica.opstinaId = -1;
  }

  animirajObavjestenje() {
    return this.closeModal == true? 'animate__animated animate__bounceOut' : 'animate__animated animate__bounceIn';
  }

  zatvoriModalObavjestenje(){
    this.closeModal = true;
    this.animirajObavjestenje();
    this.obavjestenje = setTimeout(function (){
      return false;
    },500)== 0? false : true;
  }

  private validirajFormu() {
    return this.poslovnica.adresa != null && this.poslovnica.adresa?.length > 0
    && this.poslovnica.brojTelefona != null && this.poslovnica.brojTelefona?.length > 0
    && this.poslovnica.radnoVrijemeRedovno != null && this.poslovnica.radnoVrijemeRedovno?.length > 0
    && this.poslovnica.radnoVrijemeVikend != null && this.poslovnica.radnoVrijemeVikend?.length > 0
    && this.poslovnica.lat != null && this.poslovnica.lng != null
    && this.poslovnica.opstinaId != -1;
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

  navigirajDoPoslovnica() {
    this.router.navigate(['/home-page']);
    setTimeout(()=>{
      this.router.navigate(['/home-page'], {fragment:'kontakt'});
    },1000);
  }
}
