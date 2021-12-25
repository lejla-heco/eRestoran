import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MojeNarudzbeComponent } from './moje-narudzbe.component';

describe('MojeNarudzbeComponent', () => {
  let component: MojeNarudzbeComponent;
  let fixture: ComponentFixture<MojeNarudzbeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MojeNarudzbeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MojeNarudzbeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
