import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PregledNarudzbiComponent } from './pregled-narudzbi.component';

describe('PregledNarudzbiComponent', () => {
  let component: PregledNarudzbiComponent;
  let fixture: ComponentFixture<PregledNarudzbiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PregledNarudzbiComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PregledNarudzbiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
