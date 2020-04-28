import {Component, OnInit, ViewChild} from '@angular/core';
import {EmrConfigService} from '../../../settings/services/emr-config.service';
import {Subscription} from 'rxjs/Subscription';
import {Extract} from '../../../settings/model/extract';
import {Message} from 'primeng/api';
import {TabView} from 'primeng/primeng';

@Component({
  selector: 'liveapp-mgs-extract-details',
  templateUrl: './mgs-extract-details.component.html',
  styleUrls: ['./mgs-extract-details.component.scss']
})
export class MgsExtractDetailsComponent implements OnInit {

    private _emrConfigService: EmrConfigService;
    public getEmr$: Subscription;
    public extracts: Extract[] = [];
    public extractName: string;
    public errorMessage: Message[];
    public otherMessage: Message[];
    selectedIndex = 0;
    @ViewChild(TabView) tabView: TabView;

    public constructor(emrConfigService: EmrConfigService) {
        this._emrConfigService = emrConfigService;
    }
    public ngOnInit() {
        this.getExtract();
    }
    onChange($event) {
        this.selectedIndex = $event.index;
        this.extractName = this.tabView.tabs[this.selectedIndex].header;
    }

    public getExtract(): Extract[] {
        this.getEmr$ = this._emrConfigService.getDefault()
            .subscribe(
                p => {
                    this.extracts = p.extracts.filter(x => x.docketId === 'MGS');
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
