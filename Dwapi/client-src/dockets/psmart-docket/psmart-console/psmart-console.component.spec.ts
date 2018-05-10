import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PsmartConsoleComponent } from './psmart-console.component';

describe('PsmartConsoleComponent', () => {
  let component: PsmartConsoleComponent;
  let fixture: ComponentFixture<PsmartConsoleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PsmartConsoleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PsmartConsoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
