import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HtsInvalidComponent } from './hts-invalid.component';

describe('HtsInvalidComponent', () => {
  let component: HtsInvalidComponent;
  let fixture: ComponentFixture<HtsInvalidComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HtsInvalidComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HtsInvalidComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
