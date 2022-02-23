import { Component, OnInit } from '@angular/core';
import {NarudzbaDostavljac} from "../pregled-narudzbi-dostavljac/view-models/narudzbe-dostavljac-vm";
import {NarudzbaStatus} from "../pregled-narudzbi/view-model/status-narudzbe-vm";
import {RezervacijaZaposlenik} from "./view-models/rezervacije-zaposlenik";
import {MyConfig} from "../my-config";
import {HttpClient} from "@angular/common/http";
import {RezervacijaStatus} from "./view-models/rezervacija-status";
import {UrediStatusNarudzbe} from "../pregled-narudzbi/view-model/narudzba-status-uredu-vm";
import {UrediStatusRezervacije} from "./view-models/rezervacija-status-uredi";
import {ObavljenaRezervacija} from "./view-models/rezervacija-obavljena-uredi";

@Component({
  selector: 'app-pregled-rezervacija-zaposlenik',
  templateUrl: './pregled-rezervacija-zaposlenik.component.html',
  styleUrls: ['./pregled-rezervacija-zaposlenik.component.css']
})
export class PregledRezervacijaZaposlenikComponent implements OnInit {
  currentPage: number = 1;
  totalPages: number;

  rezervacije : RezervacijaZaposlenik[] = null;
  statusi:RezervacijaStatus[]=null;

  urediStatusRezervacije:UrediStatusRezervacije= new UrediStatusRezervacije();

  obavljena:ObavljenaRezervacija= new ObavljenaRezervacija();


  obavjestenje : boolean = false;
  closeModal : boolean = false;
  obavjestenjeNaslov : string = "";
  obavjestenjeSadrzaj : string = "";

  datum:string="";

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.ucitajRezervacije();
    this.getAllStatusiRezervacije();
   // var date = new Date();
    //console.log(this.datePipe.transform(date,"dd/MM/yyyy"));
  }
  private ucitajRezervacije() {
    this.httpKlijent.get(MyConfig.adresaServera + "/Rezervacija/GetAllPagedZaposlenik/" + this.currentPage,MyConfig.httpOpcije()).subscribe((response : any)=>{
      this.totalPages = response.totalPages;
      this.rezervacije = response.dataItems;
    })
  }
  getRezervacije() {
    if (this.rezervacije == null)
      return [];

    return this.rezervacije.filter((x: RezervacijaZaposlenik)=> x.datumRezerviranjaPomocni.length==0 || (x.datumRezerviranjaPomocni.startsWith(this.datum) ));
  }
  private getAllStatusiRezervacije() {
    this.httpKlijent.get(MyConfig.adresaServera + "/StatusRezervacije/GetAll").subscribe((result:any)=>{
      this.statusi = result;
    });
  }
  createRangeStranica() {
    var niz = new Array(this.totalPages);
    for(let i : number = 0; i < this.totalPages; i++){
      niz[i] = i + 1;
    }
    return niz;
  }

  ucitajStranicu(page : number) {
    this.currentPage = page;
   this.ucitajRezervacije();
  }


  urediStatus(rezervacija:RezervacijaZaposlenik) {
    this.urediStatusRezervacije.id=rezervacija.id;
    this.urediStatusRezervacije.statusID=rezervacija.statusID;
    // this.urediStatusNarudzbe.status=narudzba.status;
    if (rezervacija.statusID != 4) {
      this.httpKlijent.post(MyConfig.adresaServera + "/StatusRezervacije/UpdateStatusZaposlenik/" + this.urediStatusRezervacije.id, this.urediStatusRezervacije, MyConfig.httpOpcije()).subscribe((result: any) => {

        this.obavjestenje = true;
        this.closeModal = false;
        this.obavjestenjeNaslov = "Uređen status";
        this.obavjestenjeSadrzaj = "Uspješno ste uredili status narudzbe";
        this.ucitajRezervacije();
      })
    }
    else{
      rezervacija.obavljena=true;

      this.obavljena.id=rezervacija.id;
      this.obavljena.obavljena=rezervacija.obavljena;
      this.httpKlijent.post(MyConfig.adresaServera + "/Rezervacija/UpdateObavljenaZaposlenik/"+this.obavljena.id,this.obavljena,MyConfig.httpOpcije()).subscribe((result:any)=> {

        this.obavjestenje = true;
        this.closeModal = false;
        this.obavjestenjeNaslov = "Rezervacija obavljena";
        this.obavjestenjeSadrzaj = "Uspješno ste označili rezervaciju obavljenom";
        this.ucitajRezervacije();
        this.ucitajRezervacije();
      });
    }
  }
  animirajObavjestenje() {
    return this.closeModal == true? 'animate__animated animate__bounceOut' : 'animate__animated animate__bounceIn';
  }

  zatvoriModalObavjestenje(){
    this.closeModal = true;
    this.animirajObavjestenje();
    setTimeout(() => {
      this.obavjestenje = false;
    },1000);
  }


  onChange(rezervacija: RezervacijaZaposlenik) {
    rezervacija.obavljena=true;

    this.obavljena.id=rezervacija.id;
    this.obavljena.obavljena=rezervacija.obavljena;
    this.httpKlijent.post(MyConfig.adresaServera + "/Rezervacija/UpdateObavljenaZaposlenik/"+this.obavljena.id,this.obavljena,MyConfig.httpOpcije()).subscribe((result:any)=> {

      this.obavjestenje = true;
      this.closeModal = false;
      this.obavjestenjeNaslov = "Rezervacija obavljena";
      this.obavjestenjeSadrzaj = "Uspješno ste označili rezervaciju obavljenom";
      this.ucitajRezervacije();
      this.ucitajRezervacije();
    })
  }
}
