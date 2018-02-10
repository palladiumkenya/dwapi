import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RestProtocolConfigComponent } from './rest-protocol-config.component';

describe('RestProtocolConfigComponent', () => {
  let component: RestProtocolConfigComponent;
  let fixture: ComponentFixture<RestProtocolConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RestProtocolConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RestProtocolConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
