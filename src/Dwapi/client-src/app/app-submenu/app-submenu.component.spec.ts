import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppSubmenuComponent } from './app-submenu.component';

describe('AppSubmenuComponent', () => {
  let component: AppSubmenuComponent;
  let fixture: ComponentFixture<AppSubmenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppSubmenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppSubmenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
