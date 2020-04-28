import { TestBed, inject } from '@angular/core/testing';

import { HtsPartnerNotificationServicesService } from './hts-partner-notification-services.service';

describe('HtsPartnerNotificationServicesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtsPartnerNotificationServicesService]
    });
  });

  it('should be created', inject([HtsPartnerNotificationServicesService], (service: HtsPartnerNotificationServicesService) => {
    expect(service).toBeTruthy();
  }));
});
