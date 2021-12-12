import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KuponComponent } from './kupon.component';

describe('KuponComponent', () => {
  let component: KuponComponent;
  let fixture: ComponentFixture<KuponComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ KuponComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(KuponComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
