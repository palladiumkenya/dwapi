import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidRecordDetailsComponent } from './valid-record-details.component';

describe('ValidRecordDetailsComponent', () => {
  let component: ValidRecordDetailsComponent;
  let fixture: ComponentFixture<ValidRecordDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValidRecordDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidRecordDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
