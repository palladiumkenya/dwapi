import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtractConfigComponent } from './extract-config.component';

describe('ExtractConfigComponent', () => {
  let component: ExtractConfigComponent;
  let fixture: ComponentFixture<ExtractConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtractConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtractConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
