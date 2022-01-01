import { Component, OnInit } from '@angular/core';
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {PosebnaPonudaStavka} from "../posebna-ponuda/view-models/posebna-ponuda-stavka-vm";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {LoginInformacije} from "../helper/login-informacije";
import {Poslovnica} from "./view-models/poslovnica-vm";

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  public posebnaPonuda: PosebnaPonudaStavka[] = null;
  public poslovnice : Poslovnica[] = null;
  loginInformacije : LoginInformacije = null;
  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  constructor(private httpKlijent : HttpClient) {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
      if (AutentifikacijaHelper.getLoginInfo().isPermisijaKorisnik) {
        this.ucitajBrojStavki(this.loginInformacije.autentifikacijaToken.korisnickiNalog.id);
      }
  }

  ngOnInit() {
    this.getPosebnaPonuda();
    this.getPoslovnice();
  }

  private getPosebnaPonuda() {
    this.httpKlijent.get(MyConfig.adresaServera + "/PosebnaPonuda/GetAll").subscribe((result:any) =>{
      this.posebnaPonuda = result;
    })
  }

  private ucitajBrojStavki(id : number) {
    this.httpKlijent.get(MyConfig.adresaServera + "/Narudzba/GetBrojStavki", MyConfig.httpOpcije()).subscribe((response : any) => {
      document.getElementById('kolicina').innerHTML = response;
    });
  }

  private getPoslovnice() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Poslovnica/GetAll").subscribe((response : any)=>{
      this.poslovnice = response;
      this.inicijalizirajGoogleMapu();
    });
  }

  inicijalizirajGoogleMapu(): void {
    const map = new google.maps.Map(
      document.getElementById("map") as HTMLElement,
      {
        zoom: 17,
        center: { lat: 43.8559, lng: 18.40725 }, //koordinate Sarajeva
      }
    );
    let vrijednost : any = new Array();
    this.poslovnice.forEach((poslovnica, i)=>{
      vrijednost.push([{ lat : poslovnica.lat, lng : poslovnica.lng}, poslovnica.adresa]);
    });

    // lat, lng i naziv svakog markera
    const tourStops: [google.maps.LatLngLiteral, string][] = vrijednost;

    // Info window koji ce dijeliti svi markeri
    const infoWindow = new google.maps.InfoWindow();

    // kreiram markere
    tourStops.forEach(([position, title], i) => {
      const marker = new google.maps.Marker({
        position,
        map,
        title: `${title}`,
        optimized: true,
      });

      // prikazi info window na klik i zumiraj na taj marker event
      marker.addListener("click", () => {
        infoWindow.close();
        infoWindow.setContent(marker.getTitle());
        infoWindow.open(marker.getMap(), marker);
        map.setZoom(19);
        map.setCenter(marker.getPosition());
      });
    });
  }

  obrisi(id : number) {
    this.httpKlijent.get(MyConfig.adresaServera + "/Poslovnica/Delete/" + id, MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.getPoslovnice();
      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov="Uspješno obrisana poslovnica";
      this.obavjestenjeSadrzaj="Uspješno ste obrisali poslovnicu na adresi: "+response.adresa;
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
    },2000)== 0? false : true;
  }

  uredi(id : number) {
    
  }
}
