import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {EmrSystem} from '../model/emr-system';
import {Extract} from '../model/extract';
import {ConfirmationService, Message, SelectItem} from 'primeng/api';
import {ExtractConfigService} from '../services/extract-config.service';
import {Subscription} from 'rxjs/Subscription';
import {forEach} from '@angular/router/src/utils/collection';

@Component({
  selector: 'liveapp-emr-docket',
  templateUrl: './emr-docket.component.html',
  styleUrls: ['./emr-docket.component.scss']
})
export class EmrDocketComponent implements OnInit, OnChanges {

    @Input() selectedEmr: EmrSystem;
    public update$: Subscription;

    public extracts: Extract[];

    public psmartExtracts: Extract[];
    public selectedPsmartExtract: Extract;

    public dwhExtracts: Extract[];
    public selectedDwhExtract: Extract;

    public cbsExtracts: Extract[];
    public selectedCbsExtract: Extract;

    public extractDialog: Extract;
    public displayDialog: boolean = false;
    public messages: Message[] = [];
    public dbs: SelectItem[] = [];
    public selectedDb: string;
    public constructor(private _confirmationService: ConfirmationService, private _emrConfigService: ExtractConfigService) {
    }

    public ngOnInit() {

    }

    public ngOnChanges(changes: SimpleChanges): void {
        this.loadExtracts();
    }

    private loadExtracts(): void {
        this.dbs = [];
        if (this.selectedEmr) {
            if (this.selectedEmr.extracts) {
                this.psmartExtracts = this.selectedEmr.extracts.filter(x => x.docketId === 'PSMART');
                this.cbsExtracts = this.selectedEmr.extracts.filter(x => x.docketId === 'CBS');
                this.dwhExtracts = this.selectedEmr.extracts.filter(x => x.docketId === 'NDWH');
            }

            if (this.selectedEmr.databaseProtocols) {
                this.selectedEmr.databaseProtocols.forEach(value => {
                        this.dbs.push({label: value.databaseName, value: value.id});
                    }
                );
            }
        }
    }

    public editExtract(extract: Extract): void {
        this.extractDialog = extract;
        this.selectedDb = extract.databaseProtocolId;
        this.displayDialog = true;
    }

    public updateExtract(): void {
        this.extractDialog.databaseProtocolId = this.selectedDb;
        this.update$ = this._emrConfigService
            .updateExtract(this.extractDialog)
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
