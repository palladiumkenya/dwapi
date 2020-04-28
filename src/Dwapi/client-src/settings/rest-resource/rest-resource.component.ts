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
    public urls: SelectItem[] = [];
    public selectedUrl: string;

    public constructor(private _confirmationService: ConfirmationService, private _emrConfigService: ExtractConfigService) {
    }

    public ngOnInit() {

    }

    public ngOnChanges(changes: SimpleChanges): void {
        this.loadResources();
    }

    private loadResources(): void {
        this.urls = [];
        if (this.selectedEmr) {
            this.restProtocols = this.selectedEmr.restProtocols;

            if (this.restProtocols && this.restProtocols.length > 0) {
                this.resources = this.restProtocols[0].resources;
                this.restProtocols.forEach(value => {
                        this.urls.push({label: value.url, value: value.id});
                    }
                );
            }
        }
    }

    public editResource(resource: Resource): void {
        this.resourceDialog = resource;
        this.selectedUrl = resource.restProtocolId;
        this.displayDialog = true;
    }

    public updateResource(): void {
        this.resourceDialog.restProtocolId = this.selectedUrl;
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
