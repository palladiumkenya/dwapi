import { TestBed, inject } from '@angular/core/testing';

import { ExtractConfigService } from './extract-config.service';

describe('ExtractConfigService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ExtractConfigService]
    });
  });

  it('should be created', inject([ExtractConfigService], (service: ExtractConfigService) => {
    expect(service).toBeTruthy();
  }));
});
