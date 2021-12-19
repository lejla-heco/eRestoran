import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PoslovnicaComponent } from './poslovnica.component';

describe('PoslovnicaComponent', () => {
  let component: PoslovnicaComponent;
  let fixture: ComponentFixture<PoslovnicaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PoslovnicaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PoslovnicaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
