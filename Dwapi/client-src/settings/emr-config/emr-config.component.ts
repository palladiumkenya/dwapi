import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreadcrumbService} from '../../app/breadcrumb.service';
import {Subscription} from 'rxjs/Subscription';
import {ConfirmationService, Message} from 'primeng/api';
import {EmrSystem} from '../model/emr-system';
import {EmrConfigService} from '../services/emr-config.service';

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
    public delete$: Subscription;

    public recordCount: number;

    public errorMessage: Message[];
    public otherMessage: Message[];

    public emrs: EmrSystem[];
    public selectedEmr: EmrSystem;
    public emrDialog: EmrSystem;

    public displayDialog: boolean;

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
            icon: 'fa fa-trash',
            accept: () => {
                this.deleteEmr(emr);
            },
            reject: () => {
                this.loadData();
            }
        });
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
