import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NdwhExtractDetailsComponent } from './ndwh-extract-details.component';

describe('NdwhExtractDetailsComponent', () => {
  let component: NdwhExtractDetailsComponent;
  let fixture: ComponentFixture<NdwhExtractDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NdwhExtractDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NdwhExtractDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
