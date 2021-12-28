import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MojiPodaciComponent } from './moji-podaci.component';

describe('MojiPodaciComponent', () => {
  let component: MojiPodaciComponent;
  let fixture: ComponentFixture<MojiPodaciComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MojiPodaciComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MojiPodaciComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
