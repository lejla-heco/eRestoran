import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../my-config";
import {Login} from "./view-models/login-vm";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  prijava : Login = new Login();
  zapamtiMe : boolean;
  constructor(private httpKlijent : HttpClient, private router : Router) { }

  ngOnInit(): void {
  }

  posaljiPodatke() {
    this.httpKlijent.post(MyConfig.adresaServera+'/Autentifikacija/Login',this.prijava).subscribe((response : any)=>{
      if (response!=null) {
        if (this.zapamtiMe) localStorage.setItem("autentifikacija-token", JSON.stringify(response));
        else sessionStorage.setItem("autentifikacija-token", JSON.stringify(response));
        this.router.navigateByUrl("/home-page");
      }
      else {
        alert("Neispravno korisničko ime i lozinka");
      }
      }
    )
  }
}
