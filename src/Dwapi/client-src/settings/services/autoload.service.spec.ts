import { TestBed, inject } from '@angular/core/testing';

import { AutoloadService } from './autoload.service';

describe('AutoloadService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AutoloadService]
    });
  });

  it('should be created', inject([AutoloadService], (service: AutoloadService) => {
    expect(service).toBeTruthy();
  }));
});
