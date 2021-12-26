import {NarudzbaStavka} from "./narudzbe-stavke-vm";
import {NarudzbaStatus} from "./status-narudzbe-vm";


export class Narudzba {
  id: number;
  cijena: number;
  datumNarucivanja: string;
  status: string;
  statusID:number;
  isKoristenKupon : boolean;
  stavke: NarudzbaStavka[];
}
