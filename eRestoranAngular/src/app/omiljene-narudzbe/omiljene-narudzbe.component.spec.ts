import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OmiljeneNarudzbeComponent } from './omiljene-narudzbe.component';

describe('OmiljeneNarudzbeComponent', () => {
  let component: OmiljeneNarudzbeComponent;
  let fixture: ComponentFixture<OmiljeneNarudzbeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OmiljeneNarudzbeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OmiljeneNarudzbeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
