import { TestBed, inject } from '@angular/core/testing';

import { NdwhPatientVisitService } from './ndwh-patient-visit.service';

describe('NdwhPatientVisitService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NdwhPatientVisitService]
    });
  });

  it('should be created', inject([NdwhPatientVisitService], (service: NdwhPatientVisitService) => {
    expect(service).toBeTruthy();
  }));
});
