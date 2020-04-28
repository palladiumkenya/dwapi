import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {EmrSystem} from '../model/emr-system';
import {Subscription} from 'rxjs/Subscription';
import {DatabaseProtocol} from '../model/database-protocol';
import {FormGroup} from '@angular/forms';
import {Message} from 'primeng/api';
import {ProtocolConfigService} from '../services/protocol-config.service';
import {RestProtocol} from '../model/rest-protocol';

@Component({
    selector: 'liveapp-rest-protocol',
    templateUrl: './rest-protocol.component.html',
    styleUrls: ['./rest-protocol.component.scss']
})
export class RestProtocolComponent implements OnInit, OnChanges {

    @Input() selectedEmr: EmrSystem;

    public saveProtocol$: Subscription;
    public verifyProtocol$: Subscription;

    public urls: RestProtocol[] = [];
    public dbDialog: RestProtocol = {};
    public dbForm: FormGroup;
    public authTypes = [
        {label: 'Token', value: 1}
    ];
    public displayDialog: boolean = false;
    public messages: Message[] = [];
    public isVerfied: boolean = false;

    public constructor(private _protocolConfigService: ProtocolConfigService) {
    }

    public ngOnInit() {
    }

    public ngOnChanges(changes: SimpleChanges): void {
        this.loadUrls();
    }

    private loadUrls(): void {
        if (this.selectedEmr) {
            if (this.selectedEmr.restProtocols) {
                this.urls = this.selectedEmr.restProtocols;
            }
        }
    }

    public newUrl(): void {
        this.dbDialog = {};
        this.displayDialog = true;
    }

    public editUrl(activeUrl: RestProtocol): void {
        this.dbDialog = activeUrl;
        this.displayDialog = true;
    }

    public saveProtocol(): void {
        this.messages = [];
        // update
        this.dbDialog.emrSystemId = this.selectedEmr.id;
        this.saveProtocol$ = this._protocolConfigService.saveRestProtocol(this.dbDialog)
            .subscribe(
                p => {
                    this.dbDialog = p;
                },
                e => {
                    this.messages = [];
                    this.messages.push({severity: 'error', summary: 'Error saving ', detail: <any>e});
                },
                () => {
                    this.messages = [];
                    this.messages.push({severity: 'success', detail: 'Saved successfully '});
                    location.reload();
                    //this.settingSavedChange.emit();
                }
            );
    }

    public verfiyProtocol(): void {
        this.messages = [];
        this.verifyProtocol$ = this._protocolConfigService.verifyRestProtocol(this.dbDialog)
            .subscribe(
                p => {
                    this.isVerfied = p;
                },
                e => {
                    this.messages = [];
                    this.messages.push({severity: 'error', summary: 'Error verifying ', detail: <any>e});
                },
                () => {
                    this.messages = [];
                    if (this.isVerfied) {
                        this.messages.push({severity: 'success', summary: 'connection was successful '});
                    } else {
                        this.messages.push({severity: 'error', summary: 'connection cannot be verfied '});
                    }
                }
            );
    }

}
