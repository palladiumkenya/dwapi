import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {BreadcrumbService} from '../../../app/breadcrumb.service';
import {Subscription} from 'rxjs/Subscription';
import {ConfirmationService, Message} from 'primeng/api';
import {EmrSystem} from '../../model/emr-system';
import {EmrConfigService} from '../../services/emr-config.service';
import {ExtractConfigService} from '../../services/extract-config.service';
import {Extract} from '../../model/extract';
@Component({
  selector: 'liveapp-extract-config',
  templateUrl: './extract-config.component.html',
  styleUrls: ['./extract-config.component.scss']
})
export class ExtractConfigComponent implements OnInit, OnChanges, OnDestroy {

    @Input() selectedEmr: EmrSystem;
    private _confirmationService: ConfirmationService;
    private _emrConfigService: ExtractConfigService;

    public loadingData: boolean;
    public get$: Subscription;
    public verify$: Subscription;
    public emrs: Extract[];

    public errorMessage: Message[];
    public otherMessage: Message[];

    public constructor(confirmationService: ConfirmationService, emrConfigService: ExtractConfigService) {

        this._confirmationService = confirmationService;
        this._emrConfigService = emrConfigService;
    }

    public ngOnChanges(changes: {[propKey: string]: SimpleChange}) {
        this.loadData();
    }

    public ngOnInit() {
    }

    public loadData(): void {

        if (!this.selectedEmr) {
            return;
        }

        this.loadingData = true;


        this.get$ = this._emrConfigService.getAll(this.selectedEmr.id, 'PSMART')
            .subscribe(
                p => {
                    this.emrs = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
                    this.loadingData = false;
                    this.emrs = null;
                },
                () => {
                    this.loadingData = false;
                }
            );
    }

    public ngOnDestroy(): void {
        if (this.get$) {
            this.get$.unsubscribe();
        }
        if (this.verify$) {
            this.verify$.unsubscribe();
        }
    }

}
