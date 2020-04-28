import { TestBed, inject } from '@angular/core/testing';

import { HtsService } from './hts.service';

describe('HtsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsService]
    });
  });

  it('should be created', inject([HtsService], (service: HtsService) => {
    expect(service).toBeTruthy();
  }));
});
