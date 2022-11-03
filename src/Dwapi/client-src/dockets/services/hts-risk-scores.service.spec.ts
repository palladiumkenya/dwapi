import { TestBed, inject } from '@angular/core/testing';

import {HtsRiskScoresService} from "./hts-risk-scores.service";

describe('HtsRiskScoresService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsRiskScoresService]
    });
  });

  it('should be created', inject([HtsRiskScoresService], (service: HtsRiskScoresService) => {
    expect(service).toBeTruthy();
  }));
});
