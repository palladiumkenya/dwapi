import { TestBed, inject } from '@angular/core/testing';

import { ProtocolConfigService } from './protocol-config.service';

describe('ProtocolConfigService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProtocolConfigService]
    });
  });

  it('should be created', inject([ProtocolConfigService], (service: ProtocolConfigService) => {
    expect(service).toBeTruthy();
  }));
});
