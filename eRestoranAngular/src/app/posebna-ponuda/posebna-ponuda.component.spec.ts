import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PosebnaPonudaComponent } from './posebna-ponuda.component';

describe('PosebnaPonudaComponent', () => {
  let component: PosebnaPonudaComponent;
  let fixture: ComponentFixture<PosebnaPonudaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PosebnaPonudaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PosebnaPonudaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
