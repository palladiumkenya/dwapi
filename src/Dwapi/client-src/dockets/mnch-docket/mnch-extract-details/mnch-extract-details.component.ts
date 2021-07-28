import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {EmrConfigService} from '../../../settings/services/emr-config.service';
import {Subscription} from 'rxjs/Subscription';
import {Extract} from '../../../settings/model/extract';
import {Message} from 'primeng/api';
import {TabView} from 'primeng/primeng';
import {EmrSystem} from "../../../settings/model/emr-system";

@Component({
  selector: 'liveapp-mnch-extract-details',
  templateUrl: './mnch-extract-details.component.html',
  styleUrls: ['./mnch-extract-details.component.scss']
})
export class MnchExtractDetailsComponent implements OnInit {

    public getEmr$: Subscription;
    public extracts: Extract[] = [];
    public extractName: string;
    public errorMessage: Message[];
    public otherMessage: Message[];
    selectedIndex = 0;
    @ViewChild(TabView) tabView: TabView;
    isLoading = true;

    @Input()
    set isInitialLoad(intial: boolean) {
        this.isLoading = intial;
    }

    @Input()
    set emr(emr: EmrSystem) {
        if (emr) {
            this.extracts = emr.extracts.filter(x => x.docketId === 'MNCH');
        }
    }

    public constructor() {
    }
    public ngOnInit() {
    }
    onChange($event) {
        this.selectedIndex = $event.index;
        this.extractName = this.tabView.tabs[this.selectedIndex].header;
    }
}
