import { TestBed, inject } from '@angular/core/testing';

import { HtsClientsService } from './hts-clients.service';

describe('HtsClientsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsClientsService]
    });
  });

  it('should be created', inject([HtsClientsService], (service: HtsClientsService) => {
    expect(service).toBeTruthy();
  }));
});
