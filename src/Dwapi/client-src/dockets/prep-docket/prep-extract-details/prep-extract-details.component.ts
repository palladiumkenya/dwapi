import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {EmrConfigService} from '../../../settings/services/emr-config.service';
import {Subscription} from 'rxjs/Subscription';
import {Extract} from '../../../settings/model/extract';
import {Message} from 'primeng/api';
import {TabView} from 'primeng/primeng';
import {EmrSystem} from "../../../settings/model/emr-system";

@Component({
  selector: 'liveapp-prep-extract-details',
  templateUrl: './prep-extract-details.component.html',
  styleUrls: ['./prep-extract-details.component.scss']
})
export class PrepExtractDetailsComponent implements OnInit {

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
            this.extracts = emr.extracts.filter(x => x.docketId === 'PREP');
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
