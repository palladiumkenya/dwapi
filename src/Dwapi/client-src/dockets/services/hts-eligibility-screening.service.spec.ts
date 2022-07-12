import { TestBed, inject } from '@angular/core/testing';

import {HtsEligibilityScreeningService} from "./hts-eligibility-screening.service";

describe('HtsEligibilityScreeningService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsEligibilityScreeningService]
    });
  });

  it('should be created', inject([HtsEligibilityScreeningService], (service: HtsEligibilityScreeningService) => {
    expect(service).toBeTruthy();
  }));
});
