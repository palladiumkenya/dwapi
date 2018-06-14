import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import {SetupService} from '../services/setup.service';
import {ConfirmationService, Message, SelectItem} from 'primeng/api';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AppDatabase} from '../model/app-database';

@Component({
  selector: 'liveapp-setup',
  templateUrl: './setup.component.html',
  styleUrls: ['./setup.component.scss']
})
export class SetupComponent implements OnInit, OnDestroy {

    private _configService: SetupService;

    public getDatabase$: Subscription;
    public getEndpoint$: Subscription;
    public verifyServer$: Subscription;
    public verifyDatabase$: Subscription;
    public saveDatabase$: Subscription;
    public verifyEndpoint$: Subscription;
    public saveEndpoint$: Subscription;

    public appDatabase: AppDatabase;
    public sysMessages: Message[];
    public dbMessages: Message[];
    public canConnect: boolean;
    public dbSaved: boolean;

    public databaseForm: FormGroup;
    public providers: SelectItem[];

    public constructor(private configService: SetupService, private fb: FormBuilder, private confirmationService: ConfirmationService) {
        this._configService = configService;

        this.providers = [
            {label: 'Select Provider', value: null},
            {label: 'MSSQL', value: 0},
            {label: 'MySQL', value: 1}
        ];

        this.databaseForm = this.fb.group({
            provider: ['', [Validators.required]],
            server: ['', [Validators.required]],
            port: [''],
            database: ['', [Validators.required]],
            user: ['', [Validators.required]],
            password: ['', [Validators.required]],
        });
    }

    public ngOnInit() {
        this.loadData();
    }

    public loadData(): void {

        this.dbMessages = [];
        this.getDatabase$ = this._configService.getDatabase()
            .subscribe(
                p => {
                    this.appDatabase = p;
                    console.log('load', this.appDatabase) ;
                },
                e => {
                    this.dbMessages.push({severity: 'error', summary: 'Error Loading', detail: <any>e});
                    console.log('b4', this.databaseForm.invalid) ;
                    this.databaseForm.reset();
                    console.log('after', this.databaseForm.invalid) ;
                },
                () => {
                    if (this.appDatabase) {
                        this.databaseForm.patchValue(this.appDatabase);
                    }
                }
            );
    }

    public verifyServer(): void {
        this.dbMessages = [];
        this.verifyServer$ = this._configService.verifyServer(this.databaseForm.value)
            .subscribe(
                p => {
                    this.canConnect = p;
                },
                e => {
                    this.dbMessages.push({ severity: 'error', summary: 'Error verifying', detail: <any>e });
                },
                () => {
                    if (this.canConnect) {
                        this.dbMessages.push({ severity: 'success', summary: 'connection successful to server ok.' });
                    }
                }
            );
    }

    public verifyDatabase(): void {
        this.dbMessages = [];
        this.verifyDatabase$ = this._configService.verifyDatabase(this.databaseForm.value)
            .subscribe(
                p => {
                    this.canConnect = p;
                },
                e => {
                    this.dbMessages.push({ severity: 'error', summary: 'Error verifying', detail: <any>e });
                },
                () => {
                    if (this.canConnect) {
                        this.dbMessages.push({ severity: 'success', summary: 'connection successful to database ok' });
                    }
                }
            );
    }


    public saveDatabase(): void {
        this.dbMessages = [];
        this.sysMessages = [];
        console.log('save', this.databaseForm.value) ;
        this.saveDatabase$ = this._configService.saveDatabase(this.databaseForm.value)
            .subscribe(
                p => {
                    this.dbSaved = p;
                },
                e => {
                    this.dbMessages.push({ severity: 'error', summary: 'Error saving', detail: <any>e });
                },
                () => {
                    if (this.dbSaved) {
                        this.dbMessages.push({ severity: 'success', summary: 'saved successfully' });
                        this.sysMessages.push({
                            severity: 'warn',
                            summary: 'Restart DWAPI inorder to effect the changes made to the system settings'
                        });
                    }
                }
            );
    }

    public ngOnDestroy(): void {

        if (this.getDatabase$) {
            this.getDatabase$.unsubscribe();
        }
        if (this.verifyServer$) {
            this.verifyServer$.unsubscribe();
        }
        if (this.verifyDatabase$) {
            this.verifyDatabase$.unsubscribe();
        }
        if (this.saveDatabase$) {
            this.saveDatabase$.unsubscribe();
        }
    }

}
