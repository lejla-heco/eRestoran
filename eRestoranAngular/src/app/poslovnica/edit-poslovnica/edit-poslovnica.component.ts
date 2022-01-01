import {Component, OnDestroy, OnInit} from '@angular/core';
import {Poslovnica} from "../../home-page/view-models/poslovnica-vm";
import {Opstina} from "../../registracija/view-models/opstina-vm";
import {MyConfig} from "../../my-config";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-edit-poslovnica',
  templateUrl: './edit-poslovnica.component.html',
  styleUrls: ['./edit-poslovnica.component.css']
})
export class EditPoslovnicaComponent implements OnInit, OnDestroy {
  id : number;
  private sub : any;
  poslovnica : Poslovnica = null;
  opstine: Opstina[] = null;
  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  constructor(private httpKlijent : HttpClient, private router : ActivatedRoute) {
  }

  ngOnInit(): void {
    this.sub = this.router.params.subscribe(params => {
      this.id = +params['id'];
    })
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

  private prikaziObavjestenje(naslov : string, sadrzaj : string) {
    this.obavjestenje = true;
    this.closeModal = false;
    this.obavjestenjeNaslov = naslov;
    this.obavjestenjeSadrzaj = sadrzaj;
  }

  azuriraj() {

  }

  ngOnDestroy(){
    this.sub.unsubscribe();
  }
}
