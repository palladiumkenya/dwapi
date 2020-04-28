import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PsmartDocketComponent } from './psmart-docket.component';

describe('PsmartDocketComponent', () => {
  let component: PsmartDocketComponent;
  let fixture: ComponentFixture<PsmartDocketComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PsmartDocketComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PsmartDocketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
