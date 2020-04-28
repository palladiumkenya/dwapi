import { TestBed, inject } from '@angular/core/testing';

import { EmrConfigService } from './emr-config.service';

describe('EmrConfigService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EmrConfigService]
    });
  });

  it('should be created', inject([EmrConfigService], (service: EmrConfigService) => {
    expect(service).toBeTruthy();
  }));
});
