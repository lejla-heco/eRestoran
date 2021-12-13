import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {MeniStavka} from "../view-models/meni-stavka-vm";
import {MyConfig} from "../../my-config";
import {HttpClient} from "@angular/common/http";
import {Ocjena} from "../view-models/ocjena-vm";
import {Router} from "@angular/router";

@Component({
  selector: 'app-star',
  templateUrl: './star.component.html',
  styleUrls: ['./star.component.css']
})
export class StarComponent implements OnInit {
  starClassName = "star-rating-blank";
  @Input() starId:any;
  @Input() rating:any;
  @Input() odabrana:MeniStavka;
  @Input() odabranaId:any;

  @Output() leave: EventEmitter<number> = new EventEmitter();
  @Output() enter: EventEmitter<number> = new EventEmitter();
  @Output() bigClick: EventEmitter<number> = new EventEmitter();


    ocjenaPoslata: Ocjena= new Ocjena();
  constructor(private httpKlijent : HttpClient, private router : Router) {}


  ngOnInit(): void {
    console.log(this.starId);
    console.log(this.rating);

    if (this.rating >= this.starId) {
      this.starClassName = "star-rating-filled";
  }

}
  onenter() {

    this.enter.emit(this.starId);
  }

  onleave() {

    this.leave.emit(this.starId);
  }

  starClicked() {

    this.bigClick.emit(this.starId);
    this.ocjenaPoslata.ocjena=this.rating;
    console.log(this.ocjenaPoslata.ocjena);

    this.httpKlijent.post(MyConfig.adresaServera+"/Meni/AddOcjena/"+this.odabrana.id,this.ocjenaPoslata).subscribe((result : any)=>{
      alert("Uspješno ste ocijenili stavku "+this.odabrana.naziv+" sa "+this.rating+" zvjezdice");
      //this.router.navigate(['/meni']);
      //window.location.replace('https://erestoran-api.p2102.app.fit.ba/meni')

    });

  }

}
