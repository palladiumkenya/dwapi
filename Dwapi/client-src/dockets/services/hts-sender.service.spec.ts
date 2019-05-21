import { TestBed, inject } from '@angular/core/testing';

import { HtsSenderService } from './hts-sender.service';

describe('HtsSenderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsSenderService]
    });
  });

  it('should be created', inject([HtsSenderService], (service: HtsSenderService) => {
    expect(service).toBeTruthy();
  }));
});
