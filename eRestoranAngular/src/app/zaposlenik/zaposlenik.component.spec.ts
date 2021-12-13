import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ZaposlenikComponent } from './zaposlenik.component';

describe('ZaposlenikComponent', () => {
  let component: ZaposlenikComponent;
  let fixture: ComponentFixture<ZaposlenikComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ZaposlenikComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ZaposlenikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
