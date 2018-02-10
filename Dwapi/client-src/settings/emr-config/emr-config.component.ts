import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreadcrumbService} from '../../app/breadcrumb.service';
import {Subscription} from 'rxjs/Subscription';
import {ConfirmationService, Message} from 'primeng/api';
import {EmrSystem} from '../model/emr-system';
import {EmrConfigService} from '../services/emr-config.service';
import {DatabaseProtocol} from '../model/database-protocol';

@Component({
  selector: 'liveapp-emr-config',
  templateUrl: './emr-config.component.html',
  styleUrls: ['./emr-config.component.scss']
})
export class EmrConfigComponent implements OnInit, OnDestroy {


    private _confirmationService: ConfirmationService;
    private _emrConfigService: EmrConfigService;

    public loadingData: boolean;

    public get$: Subscription;
    public getCount$: Subscription;
    public save$: Subscription;
    public saveProtocol$: Subscription;
    public delete$: Subscription;

    public verifyProtocol$: Subscription;

    public recordCount: number;

    public errorMessage: Message[];
    public protocolErrorMessage: Message[];
    public otherMessage: Message[];

    public emrs: EmrSystem[];
    public selectedEmr: EmrSystem;
    public selectedDatabase: DatabaseProtocol;
    public emrDialog: EmrSystem;
    public databaseTypes = [
        {label: 'Select Type', value: null},
        {label: 'Microsoft SQL', value: 1},
        {label: 'MySQL', value: 2}
    ];

    public protocolTitle: string;
    public displayDialog: boolean;
    public isVerfied: boolean;

    public constructor(public breadcrumbService: BreadcrumbService,
                       confirmationService: ConfirmationService, emrConfigService: EmrConfigService) {
        this.breadcrumbService.setItems([
            {label: 'Configuration'},
            {label: 'EMR', routerLink: ['/emrconfig']}
        ]);
        this._confirmationService = confirmationService;
        this._emrConfigService = emrConfigService;
    }

    public ngOnInit() {
        this.protocolTitle = 'Protocols';
        this.loadData();
    }

    public loadData(): void {

        this.loadingData = true;
        this.recordCount = 0;

        this.getCount$ = this._emrConfigService.getCount()
            .subscribe(
                p => {
                    this.recordCount = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
                    this.loadingData = false;
                    this.recordCount = 0;
                },
                () => {
                    // this.loading = true;
                }
            );

        this.get$ = this._emrConfigService.getAll()
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

    public newEmr(): void {
        this.emrDialog = {}
        this.displayDialog = true;
    }

    public saveRecord(): void {
        this.errorMessage = [];
        // update
        this.save$ = this._emrConfigService.save(this.emrDialog)
            .subscribe(
                p => {
                    this.selectedEmr = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error saving ', detail: <any>e});
                },
                () => {
                    this.otherMessage = [];
                    this.otherMessage.push({severity: 'success', detail: 'Saved successfully '});
                    this.displayDialog = false;
                    this.emrDialog = null;
                    this.loadData();
                }
            );
    }

    public editEmr(emr: EmrSystem): void {
        this.emrDialog = {}
        this.displayDialog = true;
    }


    private deleteEmr(emr: EmrSystem): void {
        this.errorMessage = [];
        if (!emr) {
            return;
        }

        // delete
        this.delete$ = this._emrConfigService.delete(emr.id)
            .subscribe(
                p => {
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error deleting ', detail: <any>e});
                },
                () => {
                    this.otherMessage = [];
                    this.loadData();
                });
    }

    public confirmDelete(emr: EmrSystem) {
        this._confirmationService.confirm({
            message: 'Do you want to delete record(s)?',
            header: 'Delete Confirmation',
            icon: 'ui-icon-delete',
            accept: () => {
                this.deleteEmr(emr);
            },
            reject: () => {
                this.loadData();
            }
        });
    }

    onRowSelect(event) {
        this.protocolTitle = `${event.data.name} Protcols`;
        this.showProtocols();
    }

    onRowUnselect(event) {
        this.protocolTitle = 'Protcols';
    }

    private showProtocols(): void {
        this.protocolErrorMessage = [];
        if (!this.selectedEmr) {
            return;
        }

        if (this.selectedEmr.databaseProtocols.length === 0) {
            this.selectedDatabase = {};
            this.selectedDatabase.emrSystemId = this.selectedEmr.id;
        } else {
            this.selectedDatabase = this.selectedEmr.databaseProtocols[0];
        }
    }

    public saveProtocol(): void {
        this.protocolErrorMessage = [];
        // update
        this.saveProtocol$ = this._emrConfigService.saveProtocol(this.selectedDatabase)
            .subscribe(
                p => {
                    this.selectedDatabase = p;
                },
                e => {
                    this.protocolErrorMessage = [];
                    this.protocolErrorMessage.push({severity: 'error', summary: 'Error saving ', detail: <any>e});
                },
                () => {
                    this.otherMessage = [];
                    this.otherMessage.push({severity: 'success', detail: 'Saved successfully '});
                    this.loadData();
                }
            );
    }

    public verfiyProtocol(): void {
        this.protocolErrorMessage = [];
        this.otherMessage = [];
        this.verifyProtocol$ = this._emrConfigService.verifyProtocol(this.selectedDatabase)
            .subscribe(
                p => {
                    this.isVerfied = p;
                },
                e => {
                    this.protocolErrorMessage = [];
                    this.protocolErrorMessage.push({severity: 'error', summary: 'Error verifying ', detail: <any>e});
                },
                () => {
                    this.protocolErrorMessage = [];
                    this.otherMessage = [];
                    if (this.isVerfied) {
                        this.otherMessage.push({severity: 'success', detail: 'connection was successful !'});
                        this.protocolErrorMessage.push({severity: 'success', summary: 'connection was successful '});
                    } else {
                        this.protocolErrorMessage.push({severity: 'error', summary: 'url cannot be verfied '});
                    }
                }
            );
    }

    public ngOnDestroy(): void {
        if (this.get$) {
            this.get$.unsubscribe();
        }
        if (this.getCount$) {
            this.getCount$.unsubscribe();
        }
    }
}
