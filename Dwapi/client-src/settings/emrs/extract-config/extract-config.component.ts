import {
    Component,
    Input,
    OnChanges,
    OnDestroy,
    OnInit,
    SimpleChange
} from '@angular/core';
import { BreadcrumbService } from '../../../app/breadcrumb.service';
import { Subscription } from 'rxjs/Subscription';
import { ConfirmationService, Message } from 'primeng/api';
import { EmrSystem } from '../../model/emr-system';
import { EmrConfigService } from '../../services/emr-config.service';
import { ExtractConfigService } from '../../services/extract-config.service';
import { Extract } from '../../model/extract';
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
    public update$: Subscription;
    public extracts: Extract[];
    public dwhExtracts: Extract[];
    public extractDialog: Extract;
    public selectedExtract: Extract;

    public errorMessage: Message[];
    public otherMessage: Message[];
    public displayDialog: boolean = false;

    public constructor(
        confirmationService: ConfirmationService,
        emrConfigService: ExtractConfigService
    ) {
        this._confirmationService = confirmationService;
        this._emrConfigService = emrConfigService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.loadData();
    }

    public ngOnInit() {}

    public loadData(): void {
        if (!this.selectedEmr) {
            return;
        }

        this.loadingData = true;

        this.get$ = this._emrConfigService
            .getAll(this.selectedEmr.id, 'PSMART')
            .subscribe(
                p => {
                    this.extracts = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({
                        severity: 'error',
                        summary: 'Error Loading data',
                        detail: <any>e
                    });
                    this.loadingData = false;
                    this.extracts = null;
                },
                () => {
                    this.loadingData = false;
                }
            );

        this.loadingData = true;

        this.get$ = this._emrConfigService
            .getAll(this.selectedEmr.id, 'NDWH')
            .subscribe(
                p => {
                    this.dwhExtracts = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({
                        severity: 'error',
                        summary: 'Error Loading data',
                        detail: <any>e
                    });
                    this.loadingData = false;
                    this.dwhExtracts = null;
                },
                () => {
                    this.loadingData = false;
                }
            );
    }

    public editExtract(extract: Extract): void {
        //console.log(extract);
        this.displayDialog = true;
        this.extractDialog = { ...extract };
    }
    public updateExtract(): void {
        this.update$ = this._emrConfigService
            .updateExtract(this.extractDialog)
            .subscribe(
                p => {
                    this.displayDialog = false;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({
                        severity: 'error',
                        summary: 'Error updating ',
                        detail: <any>e
                    });
                    this.loadingData = false;
                    this.extracts = null;
                },
                () => {
                    this.otherMessage = [];
                    this.otherMessage.push({
                        severity: 'success',
                        detail: 'Updated successfully '
                    });
                    this.loadData();
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
