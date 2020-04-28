import { TestBed, inject } from '@angular/core/testing';

import { NdwhPatientStatusService } from './ndwh-patient-status.service';

describe('NdwhPatientStatusService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NdwhPatientStatusService]
    });
  });

  it('should be created', inject([NdwhPatientStatusService], (service: NdwhPatientStatusService) => {
    expect(service).toBeTruthy();
  }));
});
