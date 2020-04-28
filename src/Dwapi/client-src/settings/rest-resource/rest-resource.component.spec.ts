import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RestResourceComponent } from './rest-resource.component';

describe('RestResourceComponent', () => {
  let component: RestResourceComponent;
  let fixture: ComponentFixture<RestResourceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RestResourceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RestResourceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
