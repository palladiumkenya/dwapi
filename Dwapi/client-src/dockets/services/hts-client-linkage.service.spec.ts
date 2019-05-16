import { TestBed, inject } from '@angular/core/testing';

import { HtsClientLinkageService } from './hts-client-linkage.service';

describe('HtsClientLinkageService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsClientLinkageService]
    });
  });

  it('should be created', inject([HtsClientLinkageService], (service: HtsClientLinkageService) => {
    expect(service).toBeTruthy();
  }));
});
