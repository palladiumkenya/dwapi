import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PsmartMiddlewareConsoleComponent } from './psmart-middleware-console.component';

describe('PsmartMiddlewareConsoleComponent', () => {
  let component: PsmartMiddlewareConsoleComponent;
  let fixture: ComponentFixture<PsmartMiddlewareConsoleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PsmartMiddlewareConsoleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PsmartMiddlewareConsoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
