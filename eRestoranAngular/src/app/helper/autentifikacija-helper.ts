import {LoginInformacije} from "./login-informacije";

export class AutentifikacijaHelper{

  static setLoginInfo(loginInformacije : LoginInformacije, zapamtiMe : boolean = false) : void{
    if (loginInformacije == null)
      loginInformacije = new LoginInformacije();
    if (zapamtiMe) localStorage.setItem("autentifikacija-token", JSON.stringify(loginInformacije));
    if (!zapamtiMe) sessionStorage.setItem("autentifikacija-token", JSON.stringify(loginInformacije));
  }

  static getLoginInfo():LoginInformacije{
    let x = localStorage.getItem("autentifikacija-token") == null ?
                        sessionStorage.getItem("autentifikacija-token") : localStorage.getItem("autentifikacija-token");
    if (x == null)
      return new LoginInformacije();

    try{
      let loginInformacije : LoginInformacije = JSON.parse(x);
      return loginInformacije;
    }
    catch(e){
      return new LoginInformacije();
    }
  }

  static ocistiMemoriju() {
    localStorage.removeItem("autentifikacija-token");
    sessionStorage.removeItem("autentifikacija-token");
  }
}
