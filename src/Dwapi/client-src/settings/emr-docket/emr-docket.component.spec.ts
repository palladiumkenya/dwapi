import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmrDocketComponent } from './emr-docket.component';

describe('EmrDocketComponent', () => {
  let component: EmrDocketComponent;
  let fixture: ComponentFixture<EmrDocketComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmrDocketComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmrDocketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
