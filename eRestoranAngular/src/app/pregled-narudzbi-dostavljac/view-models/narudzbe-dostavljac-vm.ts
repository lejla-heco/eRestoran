import {NarudzbaStavka} from "../../pregled-narudzbi/view-model/narudzbe-stavke-vm";

export class NarudzbaDostavljac {
  id: number;
  cijena: number;
  datumNarucivanja: string;
  status: string;
  statusID:number;
  isKoristenKupon : boolean;
  stavke: NarudzbaStavka[];
}
