import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PregledRezervacijaZaposlenikComponent } from './pregled-rezervacija-zaposlenik.component';

describe('PregledRezervacijaZaposlenikComponent', () => {
  let component: PregledRezervacijaZaposlenikComponent;
  let fixture: ComponentFixture<PregledRezervacijaZaposlenikComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PregledRezervacijaZaposlenikComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PregledRezervacijaZaposlenikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
