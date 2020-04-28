import { TestBed, inject } from '@angular/core/testing';

import { NdwhPatientPharmacyService } from './ndwh-patient-pharmacy.service';

describe('NdwhPatientPharmacyService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NdwhPatientPharmacyService]
    });
  });

  it('should be created', inject([NdwhPatientPharmacyService], (service: NdwhPatientPharmacyService) => {
    expect(service).toBeTruthy();
  }));
});
