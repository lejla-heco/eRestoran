import { Component } from '@angular/core';
import {NavigationStart, Router} from "@angular/router";
import {Uloga} from "./helper/uloga";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'eRestoranAngular';
  uloga : string = Uloga.GOST;

  constructor(router: Router) {
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        if (sessionStorage.getItem("autentifikacija-token") || localStorage.getItem("autentifikacija-token")) {
          var korisnik = JSON.parse(sessionStorage.getItem("autentifikacija-token"));

          if (korisnik == null)
            korisnik = JSON.parse(localStorage.getItem("autentifikacija-token"));

          if (korisnik.korisnickiNalog.uloga.naziv == Uloga.ADMIN) this.uloga = Uloga.ADMIN;
          else if (korisnik.korisnickiNalog.uloga.naziv == Uloga.KORISNIK) this.uloga = Uloga.KORISNIK;
          else if (korisnik.korisnickiNalog.uloga.naziv == Uloga.ZAPOSLENIK) this.uloga = Uloga.ZAPOSLENIK;
          else if (korisnik.korisnickiNalog.uloga.naziv == Uloga.DOSTAVLJAC) this.uloga = Uloga.DOSTAVLJAC;
          else this.uloga = Uloga.GOST;
        }
        else this.uloga = Uloga.GOST;
      }
    });
    router.navigateByUrl('home-page');
  }

  odjava() {
    if (sessionStorage.getItem("autentifikacija-token")) {
      sessionStorage.removeItem("autentifikacija-token");
    }
    else if (localStorage.getItem("autentifikacija-token")){
      localStorage.removeItem("autentifikacija-token");
    };
  }
}
