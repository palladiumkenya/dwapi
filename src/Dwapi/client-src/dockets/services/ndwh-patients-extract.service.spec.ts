import { TestBed, inject } from '@angular/core/testing';

import { NdwhPatientsExtractService } from './ndwh-patients-extract.service';

describe('NdwhPatientsExtractService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NdwhPatientsExtractService]
    });
  });

  it('should be created', inject([NdwhPatientsExtractService], (service: NdwhPatientsExtractService) => {
    expect(service).toBeTruthy();
  }));
});
