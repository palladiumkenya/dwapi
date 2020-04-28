import { TestBed, inject } from '@angular/core/testing';

import { HtsTestKitsService } from './hts-test-kits.service';

describe('HtsTestKitsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsTestKitsService]
    });
  });

  it('should be created', inject([HtsTestKitsService], (service: HtsTestKitsService) => {
    expect(service).toBeTruthy();
  }));
});
