import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RestProtocolComponent } from './rest-protocol.component';

describe('RestProtocolComponent', () => {
  let component: RestProtocolComponent;
  let fixture: ComponentFixture<RestProtocolComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RestProtocolComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RestProtocolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
