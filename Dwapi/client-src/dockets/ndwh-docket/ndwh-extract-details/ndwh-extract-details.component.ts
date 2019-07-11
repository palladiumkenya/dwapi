import {
    Component,
    OnInit,
    OnChanges,
    Input,
    ViewChild, SimpleChanges,
} from '@angular/core';
import {Extract} from '../../../settings/model/extract';
import {NdwhConsoleComponent} from '../ndwh-console/ndwh-console.component';
import {EmrSystem} from '../../../settings/model/emr-system';
import {EmrConfigService} from '../../../settings/services/emr-config.service';
import {Subscription} from 'rxjs/Subscription';
import {Message,} from 'primeng/api';
import {TabView, TabPanel} from 'primeng/primeng';
import {PatientExtract} from '../../models/patient-extract';
import {NdwhPatientsExtractService} from '../../services/ndwh-patients-extract.service';


@Component({
    selector: 'liveapp-ndwh-extract-details',
    templateUrl: './ndwh-extract-details.component.html',
    styleUrls: ['./ndwh-extract-details.component.scss']
})
export class NdwhExtractDetailsComponent implements OnInit, OnChanges {
    @Input() emr: EmrSystem;
    public getEmr$: Subscription;
    public extracts: Extract[] = [];
    public extractName: string;
    public errorMessage: Message[];
    public otherMessage: Message[];
    selectedIndex = 0;
    @ViewChild(TabView) tabView: TabView;

    public constructor() {
    }

    public ngOnInit() {
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (this.emr) {
            this.extracts = this.emr.extracts.filter(x => x.docketId === 'NDWH');
        }
    }

    onChange($event) {
        this.selectedIndex = $event.index;
        this.extractName = this.tabView.tabs[this.selectedIndex].header;
    }
}
