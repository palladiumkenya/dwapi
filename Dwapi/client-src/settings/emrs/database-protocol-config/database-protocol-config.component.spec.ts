import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatabaseProtocolConfigComponent } from './database-protocol-config.component';

describe('DatabaseProtocolConfigComponent', () => {
  let component: DatabaseProtocolConfigComponent;
  let fixture: ComponentFixture<DatabaseProtocolConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatabaseProtocolConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatabaseProtocolConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
