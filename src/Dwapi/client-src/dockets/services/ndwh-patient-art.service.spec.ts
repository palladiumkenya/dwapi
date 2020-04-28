import { TestBed, inject } from '@angular/core/testing';

import { NdwhPatientArtService } from './ndwh-patient-art.service';

describe('NdwhPatientArtService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NdwhPatientArtService]
    });
  });

  it('should be created', inject([NdwhPatientArtService], (service: NdwhPatientArtService) => {
    expect(service).toBeTruthy();
  }));
});
