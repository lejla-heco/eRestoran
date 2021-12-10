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
  prijava : Login = null;
  constructor(private httpKlijent : HttpClient, private router : Router) { }

  ngOnInit(): void {
  }

  posaljiPodatke() {
    this.httpKlijent.post(MyConfig.adresaServera+'/Autentifikacija/Login',this.prijava).subscribe((response : any)=>{
      localStorage.setItem("autentifikacija-token", response)

      if (response!="greska") {
        //redirect na login putanju
        this.router.navigateByUrl("/login");
      }
      else {
        alert("pogresan username i/ili password");
      }
      }
    )
  }
}
