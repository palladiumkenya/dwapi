import { TestBed, inject } from '@angular/core/testing';

import { NdwhExtractService } from './ndwh-extract.service';

describe('NdwhExtractService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NdwhExtractService]
    });
  });

  it('should be created', inject([NdwhExtractService], (service: NdwhExtractService) => {
    expect(service).toBeTruthy();
  }));
});
