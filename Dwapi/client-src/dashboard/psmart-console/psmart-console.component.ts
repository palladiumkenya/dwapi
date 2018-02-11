import {Component, Input, OnChanges, OnInit, SimpleChange} from '@angular/core';
import {Extract} from '../../settings/model/extract';
import {EmrSystem} from '../../settings/model/emr-system';

@Component({
  selector: 'liveapp-psmart-console',
  templateUrl: './psmart-console.component.html',
  styleUrls: ['./psmart-console.component.scss']
})
export class PsmartConsoleComponent implements OnInit, OnChanges {

    @Input() emr: EmrSystem;
    public loadingData: boolean;
    public extracts: Extract[];
    public recordCount: number;

    public constructor() {
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.loadData();
    }

    public ngOnInit() {
    }

    public loadData(): void {
        if (this.emr) {
            this.loadingData = true;
            this.recordCount = 0;
            this.extracts = this.emr.extracts.filter(x => x.docketId === 'PSMART');
        }
    }
}
