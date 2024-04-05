import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HtsDocketComponent } from './hts-docket.component';

describe('HtsDocketComponent', () => {
  let component: HtsDocketComponent;
  let fixture: ComponentFixture<HtsDocketComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HtsDocketComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HtsDocketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
