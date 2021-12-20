import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDostavljacComponent } from './edit-dostavljac.component';

describe('EditDostavljacComponent', () => {
  let component: EditDostavljacComponent;
  let fixture: ComponentFixture<EditDostavljacComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditDostavljacComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditDostavljacComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
