import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InlineProfileComponent } from './inline-profile.component';

describe('InlineProfileComponent', () => {
  let component: InlineProfileComponent;
  let fixture: ComponentFixture<InlineProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InlineProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InlineProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
