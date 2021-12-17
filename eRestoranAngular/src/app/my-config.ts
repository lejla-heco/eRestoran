import {HttpHeaders} from "@angular/common/http";
import {AutentifikacijaToken} from "./helper/autentifikacija-token";
import {AutentifikacijaHelper} from "./helper/autentifikacija-helper";

export class MyConfig{
  static adresaServera = "https://erestoran-api.p2102.app.fit.ba";
  static httpOpcije  = function (){

    let autentifikacijaToken:AutentifikacijaToken = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken;
    let mojtoken = "";

    if (autentifikacijaToken!=null)
      mojtoken = autentifikacijaToken.vrijednost;
    return { headers: { 'autentifikacija-token': mojtoken, }};
  }
}
