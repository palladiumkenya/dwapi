import { TestBed, inject } from '@angular/core/testing';

import { NdwhSenderService } from './ndwh-sender.service';

describe('NdwhSenderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NdwhSenderService]
    });
  });

  it('should be created', inject([NdwhSenderService], (service: NdwhSenderService) => {
    expect(service).toBeTruthy();
  }));
});
