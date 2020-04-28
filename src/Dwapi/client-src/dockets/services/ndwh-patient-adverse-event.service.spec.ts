import { TestBed, inject } from '@angular/core/testing';

import { NdwhPatientAdverseEventService } from './ndwh-patient-adverse-event.service';

describe('NdwhPatientAdverseEventService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NdwhPatientAdverseEventService]
    });
  });

  it('should be created', inject([NdwhPatientAdverseEventService], (service: NdwhPatientAdverseEventService) => {
    expect(service).toBeTruthy();
  }));
});
