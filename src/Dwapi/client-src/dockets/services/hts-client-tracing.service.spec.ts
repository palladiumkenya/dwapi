import { TestBed, inject } from '@angular/core/testing';

import { HtsClientTracingService } from './hts-client-tracing.service';

describe('HtsClientTracingService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsClientTracingService]
    });
  });

  it('should be created', inject([HtsClientTracingService], (service: HtsClientTracingService) => {
    expect(service).toBeTruthy();
  }));
});
