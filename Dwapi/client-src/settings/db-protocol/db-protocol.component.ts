import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {EmrSystem} from '../model/emr-system';
import {DatabaseProtocol} from '../model/database-protocol';
import {FormGroup} from '@angular/forms';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {ProtocolConfigService} from '../services/protocol-config.service';

@Component({
  selector: 'liveapp-db-protocol',
  templateUrl: './db-protocol.component.html',
  styleUrls: ['./db-protocol.component.scss']
})
export class DbProtocolComponent implements OnInit, OnChanges {

    @Input() selectedEmr: EmrSystem;

    public saveProtocol$: Subscription;
    public verifyProtocol$: Subscription;

    public dbs: DatabaseProtocol[] = [];
    public dbDialog: DatabaseProtocol = {};
    public dbForm: FormGroup;
    public databaseTypes = [
        {label: 'Select Type', value: null},
        {label: 'Microsoft SQL', value: 1},
        {label: 'MySQL', value: 2}
    ];
    public displayDialog: boolean = false;
    public messages: Message[] = [];
    public isVerfied: boolean = false;

    public constructor(private _protocolConfigService: ProtocolConfigService) {
    }

    public ngOnInit() {
    }

    public ngOnChanges(changes: SimpleChanges): void {
        this.loadDbs();
    }

    private loadDbs(): void {
        if (this.selectedEmr) {
            if (this.selectedEmr.databaseProtocols) {
                this.dbs = this.selectedEmr.databaseProtocols;
            }
        }
    }
    public newDatabase(): void {
        this.dbDialog = {};
        this.displayDialog = true;
    }
    public editDatabase(activeDb: DatabaseProtocol): void {
        this.dbDialog = activeDb;
        this.displayDialog = true;
    }

    public saveProtocol(): void {
        this.messages = [];
        // update
        this.dbDialog.emrSystemId = this.selectedEmr.id;
        this.saveProtocol$ = this._protocolConfigService.saveProtocol(this.dbDialog)
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

                    //this.settingSavedChange.emit();
                }
            );
    }

    public verfiyProtocol(): void {
        this.messages = [];
        this.verifyProtocol$ = this._protocolConfigService.verifyProtocol(this.dbDialog)
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
