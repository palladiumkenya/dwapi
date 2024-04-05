import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NdwhDocketComponent } from './ndwh-docket.component';

describe('NdwhDocketComponent', () => {
  let component: NdwhDocketComponent;
  let fixture: ComponentFixture<NdwhDocketComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NdwhDocketComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NdwhDocketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
