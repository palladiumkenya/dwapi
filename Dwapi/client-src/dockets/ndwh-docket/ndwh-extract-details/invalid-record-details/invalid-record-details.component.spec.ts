import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InvalidRecordDetailsComponent } from './invalid-record-details.component';

describe('InvalidPatientDetailsComponent', () => {
  let component: InvalidRecordDetailsComponent;
  let fixture: ComponentFixture<InvalidRecordDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InvalidRecordDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InvalidRecordDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
