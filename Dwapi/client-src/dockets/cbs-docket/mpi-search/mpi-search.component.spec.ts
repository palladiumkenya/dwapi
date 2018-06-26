import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MpiSearchComponent } from './mpi-search.component';

describe('MpiSearchComponent', () => {
  let component: MpiSearchComponent;
  let fixture: ComponentFixture<MpiSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MpiSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MpiSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
