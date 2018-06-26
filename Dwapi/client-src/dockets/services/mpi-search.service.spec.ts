import { TestBed, inject } from '@angular/core/testing';

import { MpiSearchService } from './mpi-search.service';

describe('MpiSearchService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MpiSearchService]
    });
  });

  it('should be created', inject([MpiSearchService], (service: MpiSearchService) => {
    expect(service).toBeTruthy();
  }));
});
