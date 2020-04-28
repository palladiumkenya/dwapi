import { TestBed, inject } from '@angular/core/testing';

import { HtsClientPartnerService } from './hts-client-partner.service';

describe('HtsClientPartnerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsClientPartnerService]
    });
  });

  it('should be created', inject([HtsClientPartnerService], (service: HtsClientPartnerService) => {
    expect(service).toBeTruthy();
  }));
});
