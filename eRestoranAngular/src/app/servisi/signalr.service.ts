import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import {MyConfig} from "../my-config";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  loginInformacije : LoginInformacije;
  podaci : any = null;
  constructor() {
    this.loginInformacije = AutentifikacijaHelper.getLoginInfo();
  }

  private hubConnection: signalR.HubConnection
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(MyConfig.adresaServera + '/notificationHub')
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('SignalR: konekcija uspostavljena'))
      .catch(err => console.log('Greška SignalR: ' + err))
  }
  public kuponNotifikacija = () => {
    this.hubConnection.on('kuponNotifikacija', (data) => {
      let id : number= this.loginInformacije.autentifikacijaToken.korisnickiNalogId;
      if (data.dobitnici.includes(id)) {
        let trenutniBroj : number = parseInt(document.getElementById("notifikacije").innerHTML);
        trenutniBroj++;
        document.getElementById("notifikacije").innerHTML = trenutniBroj.toString();
        this.podaci = {
          popust : data.popust,
          kod : data.kod
        };
      }
    });
  }
}
