import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DbProtocolComponent } from './db-protocol.component';

describe('DbProtocolComponent', () => {
  let component: DbProtocolComponent;
  let fixture: ComponentFixture<DbProtocolComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DbProtocolComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DbProtocolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
