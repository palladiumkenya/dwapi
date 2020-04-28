import { TestBed, inject } from '@angular/core/testing';

import { NdwhPatientLaboratoryService } from './ndwh-patient-laboratory.service';

describe('NdwhPatientLaboratoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NdwhPatientLaboratoryService]
    });
  });

  it('should be created', inject([NdwhPatientLaboratoryService], (service: NdwhPatientLaboratoryService) => {
    expect(service).toBeTruthy();
  }));
});
