import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CbsDocketComponent } from './cbs-docket.component';

describe('CbsDocketComponent', () => {
  let component: CbsDocketComponent;
  let fixture: ComponentFixture<CbsDocketComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CbsDocketComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CbsDocketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
