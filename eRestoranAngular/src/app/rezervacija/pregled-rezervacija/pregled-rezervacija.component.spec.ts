import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PregledRezervacijaComponent } from './pregled-rezervacija.component';

describe('PregledRezervacijaComponent', () => {
  let component: PregledRezervacijaComponent;
  let fixture: ComponentFixture<PregledRezervacijaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PregledRezervacijaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PregledRezervacijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
