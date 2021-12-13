import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OmiljenjeStavkeComponent } from './omiljenje-stavke.component';

describe('OmiljenjeStavkeComponent', () => {
  let component: OmiljenjeStavkeComponent;
  let fixture: ComponentFixture<OmiljenjeStavkeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OmiljenjeStavkeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OmiljenjeStavkeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
