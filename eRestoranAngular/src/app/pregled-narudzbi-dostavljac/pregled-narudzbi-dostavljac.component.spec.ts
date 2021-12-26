import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PregledNarudzbiDostavljacComponent } from './pregled-narudzbi-dostavljac.component';

describe('PregledNarudzbiDostavljacComponent', () => {
  let component: PregledNarudzbiDostavljacComponent;
  let fixture: ComponentFixture<PregledNarudzbiDostavljacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PregledNarudzbiDostavljacComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PregledNarudzbiDostavljacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
