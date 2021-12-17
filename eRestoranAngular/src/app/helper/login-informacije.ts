import {AutentifikacijaToken} from "./autentifikacija-token";

export class LoginInformacije {
    autentifikacijaToken: AutentifikacijaToken = null;
    isLogiran: boolean = false;
    isPermisijaKorisnik: boolean = false;
    isPermisijaAdministrator: boolean = false;
    isPermisijaZaposlenik: boolean = false;
    isPermisijaDostavljac: boolean = false;
    isPermisijaGost: boolean = true;
}

