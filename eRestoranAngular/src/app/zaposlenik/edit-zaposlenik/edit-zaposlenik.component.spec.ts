import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditZaposlenikComponent } from './edit-zaposlenik.component';

describe('EditZaposlenikComponent', () => {
  let component: EditZaposlenikComponent;
  let fixture: ComponentFixture<EditZaposlenikComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditZaposlenikComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditZaposlenikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
