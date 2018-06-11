import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistryConfigComponent } from './registry-config.component';

describe('RegistryConfigComponent', () => {
  let component: RegistryConfigComponent;
  let fixture: ComponentFixture<RegistryConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistryConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistryConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
