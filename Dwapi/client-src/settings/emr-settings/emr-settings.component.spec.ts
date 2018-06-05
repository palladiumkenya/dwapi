import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmrSettingsComponent } from './emr-settings.component';

describe('EmrSettingsComponent', () => {
  let component: EmrSettingsComponent;
  let fixture: ComponentFixture<EmrSettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmrSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmrSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
