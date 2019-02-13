import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {EmrSystem} from '../model/emr-system';
import {Subscription} from 'rxjs/Subscription';
import {Extract} from '../model/extract';
import {ConfirmationService, Message, SelectItem} from 'primeng/api';
import {ExtractConfigService} from '../services/extract-config.service';
import {Resource} from '../model/resource';
import {RestProtocol} from '../model/rest-protocol';

@Component({
    selector: 'liveapp-rest-resource',
    templateUrl: './rest-resource.component.html',
    styleUrls: ['./rest-resource.component.scss']
})
export class RestResourceComponent implements OnInit, OnChanges {

    @Input() selectedEmr: EmrSystem;
    public update$: Subscription;

    public restProtocol: RestProtocol;
    public restProtocols: RestProtocol[];

    public resources: Resource[];
    public selectedResource: Resource;

    public resourceDialog: Resource;
    public displayDialog: boolean = false;
    public messages: Message[] = [];
    public dbs: SelectItem[] = [];
    public selectedDb: string;

    public constructor(private _confirmationService: ConfirmationService, private _emrConfigService: ExtractConfigService) {
    }

    public ngOnInit() {

    }

    public ngOnChanges(changes: SimpleChanges): void {
        this.loadResources();
    }

    private loadResources(): void {
        this.dbs = [];
        if (this.selectedEmr) {

            if (this.selectedEmr.restProtocols && this.selectedEmr.restProtocols.length > 0) {
                this.resources = this.selectedEmr.restProtocols[0].resources;
            }

            if (this.selectedEmr.restProtocols) {
                this.selectedEmr.restProtocols.forEach(value => {
                        this.dbs.push({label: value.url, value: value.id});
                    }
                );
            }
        }
    }

    public editResource(extract: Extract): void {
        this.resourceDialog = extract;
        this.selectedDb = extract.databaseProtocolId;
        this.displayDialog = true;
    }

    public updateResource(): void {
        this.resourceDialog.restProtocolId = this.selectedDb;
        this.update$ = this._emrConfigService
            .updateResource(this.resourceDialog)
            .subscribe(
                p => {
                },
                e => {
                    this.messages = [];
                    this.messages.push({
                        severity: 'error',
                        summary: 'Error updating ',
                        detail: <any>e
                    });
                },
                () => {
                    this.messages = [];
                    this.messages.push({
                        severity: 'success',
                        detail: 'Updated successfully '
                    });
                }
            );
    }
}
