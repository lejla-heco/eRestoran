import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-narudzba',
  templateUrl: './narudzba.component.html',
  styleUrls: ['./narudzba.component.css']
})
export class NarudzbaComponent implements OnInit {
    stavkeNarudzbe: any;

  constructor() { }

  ngOnInit(): void {
  }

  createRange(ocjena: number) {
    let velicina = Math.round(ocjena);
    return new Array(velicina);
  }
}
