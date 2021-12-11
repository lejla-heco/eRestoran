import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {MeniStavka} from "../view-models/meni-stavka-vm";

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

  constructor() { }

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
    alert("Uspješno ste ocijenili stavku "+this.odabrana.naziv+" sa "+this.rating+" zvjezdice");
  }

}
