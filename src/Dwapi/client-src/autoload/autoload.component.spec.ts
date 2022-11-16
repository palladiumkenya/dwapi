import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutoloadComponent } from './autoload.component';

describe('AutoloadComponent', () => {
  let component: AutoloadComponent;
  let fixture: ComponentFixture<AutoloadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutoloadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutoloadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
