import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DostavljacComponent } from './dostavljac.component';

describe('DostavljacComponent', () => {
  let component: DostavljacComponent;
  let fixture: ComponentFixture<DostavljacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DostavljacComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DostavljacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
