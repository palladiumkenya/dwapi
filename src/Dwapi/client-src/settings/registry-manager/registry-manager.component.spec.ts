import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistryManagerComponent } from './registry-manager.component';

describe('RegistryManagerComponent', () => {
  let component: RegistryManagerComponent;
  let fixture: ComponentFixture<RegistryManagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistryManagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistryManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
