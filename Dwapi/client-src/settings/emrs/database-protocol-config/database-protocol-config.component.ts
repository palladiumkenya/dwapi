import {Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChange} from '@angular/core';
import {BreadcrumbService} from '../../../app/breadcrumb.service';
import {Subscription} from 'rxjs/Subscription';
import {ConfirmationService, Message} from 'primeng/api';
import {EmrSystem} from '../../model/emr-system';
import {EmrConfigService} from '../../services/emr-config.service';
import {DatabaseProtocol} from '../../model/database-protocol';
import {ProtocolConfigService} from '../../services/protocol-config.service';


@Component({
  selector: 'liveapp-database-protocol-config',
  templateUrl: './database-protocol-config.component.html',
  styleUrls: ['./database-protocol-config.component.scss']
})
export class DatabaseProtocolConfigComponent implements OnInit, OnChanges, OnDestroy {

    @Input() selectedEmr: EmrSystem;
    @Output() settingSavedChange = new EventEmitter();

    private _confirmationService: ConfirmationService;
    private _protocolConfigService: ProtocolConfigService;

    public loadingData: boolean;

    public saveProtocol$: Subscription;
    public verifyProtocol$: Subscription;
    public makeDefault$: Subscription;

    public protocolErrorMessage: Message[];
    public otherMessage: Message[];

    public selectedDatabase: DatabaseProtocol;


    public databaseTypes = [
        {label: 'Select Type', value: null},
        {label: 'Microsoft SQL', value: 1},
        {label: 'MySQL', value: 2}
    ];

    public isVerfied: boolean;
    public isMadeDefault: boolean;


    public constructor(public breadcrumbService: BreadcrumbService,
                       confirmationService: ConfirmationService, protocolConfigService: ProtocolConfigService) {
        this.breadcrumbService.setItems([
            {label: 'Configuration'},
            {label: 'EMR', routerLink: ['/emrconfig']}
        ]);
        this._confirmationService = confirmationService;
        this._protocolConfigService = protocolConfigService;
    }

    ngOnChanges(changes: {[propKey: string]: SimpleChange}) {
        this.showProtocols();
    }

    public ngOnInit() {
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
        this.saveProtocol$ = this._protocolConfigService.saveProtocol(this.selectedDatabase)
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
                    this.otherMessage.push({severity: 'success', detail: 'Saved successfully '})
                    this.settingSavedChange.emit();
                }
            );
    }

    public verfiyProtocol(): void {
        this.protocolErrorMessage = [];
        this.otherMessage = [];
        this.verifyProtocol$ = this._protocolConfigService.verifyProtocol(this.selectedDatabase)
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

    public makeDefault(): void {
        console.log(this.selectedEmr);
        this.protocolErrorMessage = [];
        this.otherMessage = [];
        this.makeDefault$ = this._protocolConfigService.makeEmrDefault(this.selectedEmr)
            .subscribe(
                p => {
                    this.isMadeDefault = p;
                },

                e => {
                    this.protocolErrorMessage = [];
                    this.protocolErrorMessage.push({
                        severity: 'error',
                        summary: `Error setting ${this.selectedEmr.name} as default `,
                        detail: <any>e
                    });
                },

                () => {
                    this.protocolErrorMessage = [];
                    this.otherMessage = [];
                    if (this.isMadeDefault) {
                        this.otherMessage.push({severity: 'success', detail: `${this.selectedEmr.name} was set as default successfully`});
                        this.protocolErrorMessage.push({
                            severity: 'success',
                            summary: `${this.selectedEmr.name} was set as default successfully`
                        });
                    } else {
                        this.protocolErrorMessage.push({
                            severity: 'error', summary: `${this.selectedEmr.name}cannot be set as default `
                        });
                    }
                }
            );
    }

    public confirmDefault() {
        console.log(this.selectedEmr.id);
        this._confirmationService.confirm({

            message: `Do you want to set ${this.selectedEmr.name} as default?`,
            header: 'Confirmation',
            icon: 'ui-icon-done-all',
            accept: () => {
                this.makeDefault();
            },
            reject: () => {

            }
        });
    }

    public ngOnDestroy(): void {
        if (this.saveProtocol$) {
            this.saveProtocol$.unsubscribe();
        }
        if (this.verifyProtocol$) {
            this.verifyProtocol$.unsubscribe();
        }
    }

}
