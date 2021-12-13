import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-omiljenje-stavke',
  templateUrl: './omiljenje-stavke.component.html',
  styleUrls: ['./omiljenje-stavke.component.css']
})
export class OmiljenjeStavkeComponent implements OnInit {
  omiljeneStavke: any;

  constructor() { }

  ngOnInit(): void {
  }

}
