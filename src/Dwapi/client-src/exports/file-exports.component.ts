import { Component, OnDestroy, OnInit } from "@angular/core";
import { ConfirmationService, Message } from 'primeng/api';
import { Subscription } from 'rxjs/Subscription';
import {CrsService} from '../dockets/services/crs.service';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import { ProgressStatusEnum, ProgressStatus } from '../progress-status.model';




@Component({
    selector: 'liveapp-file-exports',
    templateUrl: './file-exports.component.html',
    styleUrls: ['./file-exports.component.scss']
   
})
export class exportComponent implements OnInit, OnDestroy {

    private _hubConnection: HubConnection | undefined;
    public async: any;

    private _confirmationService: ConfirmationService;

   

    public getEmr$: Subscription;
    public load$: Subscription;
    public getStatus$: Subscription;
    public get$: Subscription;
    public getCount$: Subscription;
    public getFiles$: Subscription;
    public getall$: Subscription;
    public getallCount$: Subscription;
    public getCrsExtractSummery$: Subscription;
    public loadRegistry$: Subscription;
    public sendManifest$: Subscription;
    public send$: Subscription;
    public filesExported: string[] =[]; 
    public emrVersion: string;
    public minEMRVersion: string;
    

    public files: string[];   
    public fileInDownload: string;
    public percentage: number;
    public showProgress: boolean;
    public showDownloadError: boolean;
    public showUploadError: boolean;

    public messages: Message[];
    public metricMessages: Message[];
    public notifications: Message[];
    public canLoad: boolean = false;
    public canSend: boolean = false;
    public canExport: boolean = false;
    public loading: boolean = false;
    public loadingAll: boolean = false;
    public canSendCrs: boolean = false;
    public sending: boolean = false;
    public sendingManifest: boolean = false;
    public exporting: boolean = false;
    public exportingManifest: boolean = false;
    public recordCount = 0;
    public allrecordCount = 0;
   
    private sdk: string[] = [];
    public colorMappings: any[] = [];
    rowStyleMap: { [key: string]: string };
   

    public constructor(confirmationService: ConfirmationService, private CrsService: CrsService) {
       
        this._confirmationService = confirmationService;
        
    }

    public ngOnInit() {
        this.loadFiles();
        this.getFiles();
       
    }
    private getFiles() {
        this.CrsService.getFiles().subscribe(
            data => {
                this.files = data;
            }
        );
    }




    loadFiles() {

        this.getFiles$ = this.CrsService.getExportedFiles()
            .subscribe(
                p => {
                    this.filesExported = p;


                },
                e => {
                    this.messages = [];
                    this.messages.push({ severity: 'error', summary: 'Error loading files ', detail: <any>e });
                },
                () => {
                }
            );

    }
   
 
    public downloadStatus(event: ProgressStatus) {
        switch (event.status) {
            case ProgressStatusEnum.START:
                this.showDownloadError = false;
                break;
            case ProgressStatusEnum.IN_PROGRESS:
                this.showProgress = true;
                this.percentage = event.percentage;
                break;
            case ProgressStatusEnum.COMPLETE:
                this.showProgress = false;
                break;
            case ProgressStatusEnum.ERROR:
                this.showProgress = false;
                this.showDownloadError = true;
                break;
        }
    } 


    ngOnDestroy(): void {
        if (this.get$) {
            this.get$.unsubscribe();
        }
        if (this.getFiles$) {
            this.getFiles$.unsubscribe();
        }
        if (this.getCount$) {
            this.getCount$.unsubscribe();
        }
        
    }


    displayFiles() {
        this.getFiles$ = this.CrsService.getExportedFiles()
            .subscribe(
                p => {
                    this.filesExported = p;
                   
                    
                },
                e => {
                    this.messages = [];
                    this.messages.push({ severity: 'error', summary: 'Error loading files ', detail: <any>e });
                },
                () => {
                }
            );
    }
}
