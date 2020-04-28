import { TestBed, inject } from '@angular/core/testing';

import { RegistryConfigService } from './registry-config.service';

describe('RegistryConfigService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RegistryConfigService]
    });
  });

  it('should be created', inject([RegistryConfigService], (service: RegistryConfigService) => {
    expect(service).toBeTruthy();
  }));
});
