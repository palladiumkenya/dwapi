import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HtsConsoleComponent } from './hts-console.component';

describe('HtsConsoleComponent', () => {
  let component: HtsConsoleComponent;
  let fixture: ComponentFixture<HtsConsoleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HtsConsoleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HtsConsoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
