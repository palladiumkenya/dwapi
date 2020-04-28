import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HtsValidComponent } from './hts-valid.component';

describe('HtsValidComponent', () => {
  let component: HtsValidComponent;
  let fixture: ComponentFixture<HtsValidComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HtsValidComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HtsValidComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
