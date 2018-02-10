import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmrConfigComponent } from './emr-config.component';

describe('EmrConfigComponent', () => {
  let component: EmrConfigComponent;
  let fixture: ComponentFixture<EmrConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmrConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmrConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
