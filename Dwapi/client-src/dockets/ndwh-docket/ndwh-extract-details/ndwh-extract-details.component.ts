import {
    Component,
    OnInit,
    OnChanges,
    Input,
} from '@angular/core';
import {Extract} from '../../../settings/model/extract';
import { NdwhConsoleComponent } from '../ndwh-console/ndwh-console.component';
import { EmrSystem } from '../../../settings/model/emr-system';
import { EmrConfigService } from '../../../settings/services/emr-config.service';
import { Subscription } from 'rxjs/Subscription';
import { Message } from 'primeng/api';


@Component({
    selector: 'liveapp-ndwh-extract-details',
    templateUrl: './ndwh-extract-details.component.html',
    styleUrls: ['./ndwh-extract-details.component.scss']
})
export class NdwhExtractDetailsComponent implements OnInit {
    private _emrConfigService: EmrConfigService;
    public getEmr$: Subscription;
    public extracts: Extract[] = [];
    public errorMessage: Message[];
    public otherMessage: Message[];
    public constructor(emrConfigService: EmrConfigService) {
        this._emrConfigService = emrConfigService;
     }
    public ngOnInit() {
        this.getExtract();
    }

    public getExtract(): Extract[] {
        this.getEmr$ = this._emrConfigService.getDefault()
          .subscribe(
              p => {
                  this.extracts = p.extracts.filter(x => x.docketId === 'NDWH');
                  return this.extracts;
              },
              e => {
                  this.errorMessage = [];
                  this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
              },
              () => {

              }
          );
          return this.extracts;
    }
}
