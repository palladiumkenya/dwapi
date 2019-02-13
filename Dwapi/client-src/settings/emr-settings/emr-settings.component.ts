import {Component, OnDestroy, OnInit} from '@angular/core';
import {EmrConfigService} from '../services/emr-config.service';
import {ConfirmationService, MenuItem, Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {EmrSystem} from '../model/emr-system';
import {BreadcrumbService} from '../../app/breadcrumb.service';

@Component({
    selector: 'liveapp-emr-settings',
    templateUrl: './emr-settings.component.html',
    styleUrls: ['./emr-settings.component.scss']
})
export class EmrSettingsComponent implements OnInit, OnDestroy {

    private _confirmationService: ConfirmationService;
    private _emrConfigService: EmrConfigService;

    public loadingData: boolean;

    public get$: Subscription;
    public getCount$: Subscription;
    public save$: Subscription;
    public delete$: Subscription;
    public makeDefault$: Subscription;

    public recordCount: number;

    public messages: Message[];
    public otherMessage: Message[];

    public emrs: EmrSystem[];
    public selectedEmr: EmrSystem;
    public emrDialog: EmrSystem;
    public protocolTitle: string;
    public docketTitle: string;
    public resourceTitle: string;
    public displayDialog: boolean;

    public canMakeDefault: boolean;
    public isMadeDefault: boolean;

    public items: MenuItem[];

    public constructor(public breadcrumbService: BreadcrumbService,
                       confirmationService: ConfirmationService, emrConfigService: EmrConfigService) {
        this.breadcrumbService.setItems([
            {label: 'Configuration'},
            {label: 'EMR', routerLink: ['/emrconfig']}
        ]);
        this._confirmationService = confirmationService;
        this._emrConfigService = emrConfigService;
        this.items = [
            {label: 'Default', icon: 'ui-icon-check', command: () => this.makeDefault()},
            {label: 'Delete', icon: 'fa-close', command: () => this.confirmDelete()}
        ];
    }

    public ngOnInit() {
        this.protocolTitle = 'Protocols';
        this.docketTitle = 'Dockets';
        this.resourceTitle = 'Resources';
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
                    this.messages = [];
                    this.messages.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
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
                    this.messages = [];
                    this.messages.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
                    this.loadingData = false;
                    this.emrs = null;
                },
                () => {
                    this.loadingData = false;
                    console.log(this.emrs);
                }
            );
    }

    public newEmr(): void {
        this.emrDialog = {};
        this.displayDialog = true;
    }

    public saveRecord(): void {
        this.messages = [];
        // update
        this.save$ = this._emrConfigService.save(this.emrDialog)
            .subscribe(
                p => {
                    this.selectedEmr = p;
                },
                e => {
                    this.messages = [];
                    this.messages.push({severity: 'error', summary: 'Error saving ', detail: <any>e});
                },
                () => {
                    this.messages = [];
                    this.messages.push({severity: 'success', detail: 'Saved successfully '});
                    this.displayDialog = false;
                    this.emrDialog = null;
                    this.loadData();
                }
            );
    }

    public editEmr(emr: EmrSystem): void {
        this.emrDialog = {};
        this.displayDialog = true;
    }


    private deleteEmr(emr: EmrSystem): void {
        this.messages = [];
        if (!emr) {
            return;
        }

        // delete
        this.delete$ = this._emrConfigService.delete(emr.id)
            .subscribe(
                p => {
                },
                e => {
                    this.messages = [];
                    this.messages.push({severity: 'error', summary: 'Error deleting ', detail: <any>e});
                },
                () => {
                    this.messages = [];
                    this.loadData();
                });
    }

    public selectEmr(emr: EmrSystem) {
        this.selectedEmr = emr;
    }

    public makeDefault(): void {
        if (!this.selectedEmr) {
            return;
        }
        this.isMadeDefault = false;
        this.messages = [];
        this.makeDefault$ = this._emrConfigService.makeEmrDefault(this.selectedEmr)
            .subscribe(
                p => {
                    this.isMadeDefault = p;
                },
                e => {

                    this.messages.push({
                        severity: 'error',
                        summary: `Error setting ${this.selectedEmr.name} as default `,
                        detail: <any>e
                    });
                },

                () => {
                    if (this.isMadeDefault) {
                        this.messages.push({
                            severity: 'success',
                            summary: `${this.selectedEmr.name} was set as default successfully`
                        });
                    } else {
                        this.messages.push({
                            severity: 'error', summary: `${this.selectedEmr.name}cannot be set as default `
                        });
                    }
                }
            );
    }

    public confirmDelete() {

        if (!this.selectedEmr) {
            return;
        }
        this._confirmationService.confirm({
            message: 'Do you want to delete record(s)?',
            header: 'Delete Confirmation',
            icon: 'ui-icon-delete',
            accept: () => {
                this.deleteEmr(this.selectedEmr);
            },
            reject: () => {
                this.loadData();
            }
        });
    }

    onRowSelect(event) {
        this.protocolTitle = `${event.data.name} Protocols`;
        this.docketTitle = `${event.data.name} Dockets`;
        this.resourceTitle = `${event.data.name} Resources`;
    }

    onRowUnselect(event) {
        this.protocolTitle = 'Protocols';
        this.docketTitle = 'Dockets';
        this.docketTitle = 'Resources';
    }

    public ngOnDestroy(): void {
        if (this.get$) {
            this.get$.unsubscribe();
        }
        if (this.getCount$) {
            this.getCount$.unsubscribe();
        }
        if (this.makeDefault$) {
            this.makeDefault$.unsubscribe();
        }
    }
}
