import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";
import {Login} from "./view-models/login-vm";
import {Router} from "@angular/router";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  prijava : Login = new Login();
  zapamtiMe : boolean;
  fieldText: boolean;
  constructor(private httpKlijent : HttpClient, private router : Router) { }

  ngOnInit(): void {
  }

  posaljiPodatke() {
    this.httpKlijent.post(MyConfig.adresaServera+'/Autentifikacija/Login',this.prijava).subscribe((response : any)=>{
      if (response.isLogiran) {
        response.isPermisijaGost = false;
        AutentifikacijaHelper.setLoginInfo(response, this.zapamtiMe);
        this.router.navigateByUrl("home-page");
      }
      else {
        AutentifikacijaHelper.setLoginInfo(null);
        alert("Neispravno korisničko ime i lozinka");
      }
      }
    )
  }

  prikaziRegistraciju() {
    this.router.navigate(['/registracija']);
  }

  prikaziSakrij() {
    this.fieldText = !this.fieldText;
  }
}
