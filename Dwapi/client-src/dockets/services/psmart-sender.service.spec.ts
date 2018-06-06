import { TestBed, inject } from '@angular/core/testing';

import { PsmartSenderService } from './psmart-sender.service';

describe('PsmartSenderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PsmartSenderService]
    });
  });

  it('should be created', inject([PsmartSenderService], (service: PsmartSenderService) => {
    expect(service).toBeTruthy();
  }));
});
