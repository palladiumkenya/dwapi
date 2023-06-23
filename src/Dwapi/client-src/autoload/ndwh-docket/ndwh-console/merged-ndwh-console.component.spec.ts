import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MergedNdwhConsoleComponent } from './merged-ndwh-console.component';

describe('MergedNdwhConsoleComponent', () => {
  let component: MergedNdwhConsoleComponent;
  let fixture: ComponentFixture<MergedNdwhConsoleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MergedNdwhConsoleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MergedNdwhConsoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
