import { TestBed, inject } from '@angular/core/testing';

import { HtsClientsLinkageService } from './hts-clients-linkage.service';

describe('HtsClientsLinkageService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsClientsLinkageService]
    });
  });

  it('should be created', inject([HtsClientsLinkageService], (service: HtsClientsLinkageService) => {
    expect(service).toBeTruthy();
  }));
});
