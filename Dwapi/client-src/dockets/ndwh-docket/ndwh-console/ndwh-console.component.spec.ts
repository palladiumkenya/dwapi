import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NdwhConsoleComponent } from './ndwh-console.component';

describe('NdwhConsoleComponent', () => {
  let component: NdwhConsoleComponent;
  let fixture: ComponentFixture<NdwhConsoleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NdwhConsoleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NdwhConsoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
