import { TestBed, inject } from '@angular/core/testing';

import { AppDetailsService } from './app-details.service';

describe('AppDetailsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AppDetailsService]
    });
  });

  it('should be created', inject([AppDetailsService], (service: AppDetailsService) => {
    expect(service).toBeTruthy();
  }));
});
