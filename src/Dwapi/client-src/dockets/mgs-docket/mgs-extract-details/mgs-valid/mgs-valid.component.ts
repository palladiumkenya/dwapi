import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {PageModel} from '../../../models/page-model';
import {MetricMigrationService} from '../../../services/metric-migration.service';

@Component({
    selector: 'liveapp-mgs-valid',
    templateUrl: './mgs-valid.component.html',
    styleUrls: ['./mgs-valid.component.scss']
})
export class MgsValidComponent implements OnInit, OnDestroy {
    public validExtracts: any[] = [];
    public recordCount = 0;
    public cols: any[];
    public errorMessage: Message[];
    public otherMessage: Message[];
    public getValid$: Subscription;
    public getValidCount$: Subscription;
    public pageModel: PageModel;
    public initialRows: number = 10;
    private exName: string;
    public loadingData = false;

    constructor(private migrationService: MetricMigrationService) {
    }

    get extract(): string {
        return this.exName;
    }

    @Input()
    set extract(extract: string) {
        if (extract) {
            this.exName = extract;
            this.cols = [];
            this.validExtracts = [];
            this.pageModel = {
                page: 1,
                pageSize: this.initialRows
            };
            this.getColumns();
            this.getValidExtracts();
        }
    }

    ngOnInit() {
        this.exName = 'Migration';
        this.pageModel = {
            page: 1,
            pageSize: this.initialRows
        };
        this.getMigrationColumns();
        this.getValidMigrationClients();
    }

    public getValidExtracts(): void {
        if (this.extract === 'Migration') {
            this.getValidMigrationClients();
        }
    }

    private getColumns(): void {
        if (this.extract === 'Migration') {
            this.getMigrationColumns();
        }
    }

    private getValidMigrationClients(): void {
        this.loadingData = true;
        this.getValidCount$ = this.migrationService.loadValidCount().subscribe(
            p => {
                this.recordCount = p;
                this.getValidMigrationExtracts();
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
                this.loadingData = false;
            }
        );
    }

    private getValidMigrationExtracts(): void {
        this.loadingData = true;
        this.getValid$ = this.migrationService.loadValid(this.pageModel).subscribe(
            p => {
                this.validExtracts = p;
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
                this.loadingData = false;
            }
        );
    }

    private getMigrationColumns(): void {
        this.cols = [
            { field: 'metricId', header: 'MetricId' },
            { field: 'dataset', header: 'Dataset' },
            { field: 'metric', header: 'Metric' },
            { field: 'metricValue', header: 'MetricValue' },
            { field: 'createDate', header: 'CreateDate' },
            { field: 'siteCode', header: 'SiteCode' },
            { field: 'facilityName', header: 'FacilityName' },
        ];
    }

    pageView(event: any) {
        this.pageModel = {
            page: event.first / event.rows + 1,
            pageSize: event.rows,
            sortField: event.sortField,
            sortOrder: event.sortOrder
        };
        this.getColumns();
        this.getValidExtracts();
    }

    ngOnDestroy(): void {
        if (this.getValid$) {
            this.getValid$.unsubscribe();
        }
        if (this.getValidCount$) {
            this.getValidCount$.unsubscribe();
        }
    }
}
