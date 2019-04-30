import { TestBed, inject } from '@angular/core/testing';

import { HtsClientService } from './hts-client.service';

describe('HtsClientService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsClientService]
    });
  });

  it('should be created', inject([HtsClientService], (service: HtsClientService) => {
    expect(service).toBeTruthy();
  }));
});
