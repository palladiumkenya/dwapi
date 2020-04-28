import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HtsExtractDetailsComponent } from './hts-extract-details.component';

describe('HtsExtractDetailsComponent', () => {
  let component: HtsExtractDetailsComponent;
  let fixture: ComponentFixture<HtsExtractDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HtsExtractDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HtsExtractDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
