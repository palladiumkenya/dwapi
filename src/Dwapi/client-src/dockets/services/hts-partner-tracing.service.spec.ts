import { TestBed, inject } from '@angular/core/testing';

import { HtsPartnerTracingService } from './hts-partner-tracing.service';

describe('HtsPartnerTracingService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsPartnerTracingService]
    });
  });

  it('should be created', inject([HtsPartnerTracingService], (service: HtsPartnerTracingService) => {
    expect(service).toBeTruthy();
  }));
});
