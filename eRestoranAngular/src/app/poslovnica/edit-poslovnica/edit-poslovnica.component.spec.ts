import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPoslovnicaComponent } from './edit-poslovnica.component';

describe('EditPoslovnicaComponent', () => {
  let component: EditPoslovnicaComponent;
  let fixture: ComponentFixture<EditPoslovnicaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditPoslovnicaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPoslovnicaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
