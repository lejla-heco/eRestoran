import {NarudzbaStavka} from "./narudzba-stavka";

export class Narudzba {
  id: number;
  cijena: number;
  omiljeno : boolean;
  stavke: NarudzbaStavka[];
}
