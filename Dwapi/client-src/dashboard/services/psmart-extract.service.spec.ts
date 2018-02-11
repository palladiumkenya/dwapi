import { TestBed, inject } from '@angular/core/testing';

import { PsmartExtractService } from './psmart-extract.service';

describe('PsmartExtractService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PsmartExtractService]
    });
  });

  it('should be created', inject([PsmartExtractService], (service: PsmartExtractService) => {
    expect(service).toBeTruthy();
  }));
});
