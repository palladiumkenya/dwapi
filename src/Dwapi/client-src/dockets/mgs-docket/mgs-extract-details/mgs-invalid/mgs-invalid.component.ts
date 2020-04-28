import {Component, Input, OnChanges, OnInit, SimpleChange} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import { Message } from 'primeng/api';
import {MetricMigrationService} from '../../../services/metric-migration.service';

@Component({
    selector: 'liveapp-mgs-invalid',
    templateUrl: './mgs-invalid.component.html',
    styleUrls: ['./mgs-invalid.component.scss']
})
export class MgsInvalidComponent implements OnInit, OnChanges {

    @Input() extract: string;
    public invalidExtracts: any[] = [];
    public cols: any[];
    public getInvalid$: Subscription;
    public errorMessage: Message[];
    public otherMessage: Message[];

    constructor(private migrationService: MetricMigrationService) {
       }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.cols = [];
        this.invalidExtracts = [];
        this.getColumns();
        this.getInvalidExtracts();
    }

    ngOnInit() {
        this.getMigrationColumns();
        this.getMigrationExtracts();
    }

    public getInvalidExtracts(): void {
        if (this.extract === 'Migration') {
            this.getMigrationExtracts();
        }
    }

    private getColumns(): void {
        if (this.extract === 'Migration') {
            this.getMigrationColumns();
        }
    }

    private getMigrationExtracts(): void {
        this.getInvalid$ = this.migrationService.loadValidations().subscribe(
            p => {
                this.invalidExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
            }
        );
    }

    private getMigrationColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'metricId', header: 'MetricId' },
            { field: 'dataset', header: 'Dataset' },
            { field: 'metric', header: 'Metric' },
            { field: 'metricValue', header: 'MetricValue' },
            { field: 'createDate', header: 'CreateDate' },
            { field: 'siteCode', header: 'SiteCode' },
            { field: 'facilityName', header: 'FacilityName' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' }
        ];
    }
}
