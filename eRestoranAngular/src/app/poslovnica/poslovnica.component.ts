import { Component, OnInit } from '@angular/core';
import {Poslovnica} from "../home-page/view-models/poslovnica-vm";
import {Opstina} from "../registracija/view-models/opstina-vm";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-poslovnica',
  templateUrl: './poslovnica.component.html',
  styleUrls: ['./poslovnica.component.css']
})
export class PoslovnicaComponent implements OnInit {
  poslovnica : Poslovnica = new Poslovnica();
  opstine: Opstina[] = null;
  constructor(private httpKlijent : HttpClient) { }

  ngOnInit(): void {
    this.inicijalizirajGoogleMapu();
    this.ucitajOpstine();
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
    this.httpKlijent.post(MyConfig.adresaServera + "/Poslovnica/Add",this.poslovnica,MyConfig.httpOpcije()).subscribe((response : any)=>{
      alert("Uspjesno dodana nova poslovnica!");
      this.ocistiFormu();
    });
  }

  ocistiFormu(){
    this.poslovnica.adresa = null;
    this.poslovnica.brojTelefona = null;
    this.poslovnica.radnoVrijemeRedovno = null;
    this.poslovnica.radnoVrijemeVikend = null;
    this.poslovnica.lat = null;
    this.poslovnica.lng = null;
    this.poslovnica.opstinaId = null;
  }
}
