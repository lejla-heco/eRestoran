import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoviZaposlenikComponent } from './novi-zaposlenik.component';

describe('NoviZaposlenikComponent', () => {
  let component: NoviZaposlenikComponent;
  let fixture: ComponentFixture<NoviZaposlenikComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NoviZaposlenikComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NoviZaposlenikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
