import {Stavka} from "./moja-narudzba-stavka-vm";


export class MojaNarudzba {
  id: number;
  cijena: number;
  datumNarucivanja: string;
  status: string;
  isKoristenKupon : boolean;
  stavke: Stavka[];
}
