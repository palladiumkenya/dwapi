import { TestBed, inject } from '@angular/core/testing';

import { HtsClientTestsService } from './hts-client-tests.service';

describe('HtsClientTestsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsClientTestsService]
    });
  });

  it('should be created', inject([HtsClientTestsService], (service: HtsClientTestsService) => {
    expect(service).toBeTruthy();
  }));
});
