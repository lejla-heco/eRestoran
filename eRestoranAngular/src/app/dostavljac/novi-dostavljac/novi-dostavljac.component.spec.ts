import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoviDostavljacComponent } from './novi-dostavljac.component';

describe('NoviDostavljacComponent', () => {
  let component: NoviDostavljacComponent;
  let fixture: ComponentFixture<NoviDostavljacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NoviDostavljacComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NoviDostavljacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
