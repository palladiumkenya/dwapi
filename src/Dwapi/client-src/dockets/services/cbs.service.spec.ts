import { TestBed, inject } from '@angular/core/testing';

import { CbsService } from './cbs.service';

describe('CbsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CbsService]
    });
  });

  it('should be created', inject([CbsService], (service: CbsService) => {
    expect(service).toBeTruthy();
  }));
});
