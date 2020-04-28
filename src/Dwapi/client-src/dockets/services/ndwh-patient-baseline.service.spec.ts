import { TestBed, inject } from '@angular/core/testing';

import { NdwhPatientBaselineService } from './ndwh-patient-baseline.service';

describe('NdwhPatientBaselineService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NdwhPatientBaselineService]
    });
  });

  it('should be created', inject([NdwhPatientBaselineService], (service: NdwhPatientBaselineService) => {
    expect(service).toBeTruthy();
  }));
});
