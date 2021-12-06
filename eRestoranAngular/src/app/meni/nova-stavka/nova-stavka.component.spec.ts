import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NovaStavkaComponent } from './nova-stavka.component';

describe('NovaStavkaComponent', () => {
  let component: NovaStavkaComponent;
  let fixture: ComponentFixture<NovaStavkaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NovaStavkaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NovaStavkaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
