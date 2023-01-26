import { Component,OnInit } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import { SendEvent } from '../settings/model/send-event';
import { Uploader } from '../entities/uploader';
import { UploadQueue } from '../entities/uploadqueue';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import { environment } from '../environments/environment';
import { Extract } from '../settings/model/extract';
import { ConfirmationService, Message } from 'primeng/api';
import { ExtractEvent } from '../settings/model/extract-event';
import { Subject } from 'rxjs';

import { CentralRegistry } from '../settings/model/central-registry';

@Component({
    selector: 'app-upload',
    templateUrl: './file-upload.component.html',
    styleUrls: ['./file-upload.component.scss']
})
export class UploadComponent implements OnInit {

    public sendEvent: SendEvent = {};
    public sending: boolean = false;
    private _hubConnectionMpi: HubConnection | undefined;
    private _hubConnection: HubConnection | undefined;
    public notifications: Message[];
    public extracts: Extract[] = [];
    public errorMessage: Message[];
    public currentExtract: Extract;
    private extractEvent: ExtractEvent;   
    public otherMessage: Message[];
    private progressSubject = new Subject<number>();
    public progress$ = this.progressSubject.asObservable();
    progresss = 0;
    public warningMessage: Message[];

    smartMode = false;

    //get progress: get overall progress
    get progress(): number {
        let psum = 0;

        for (let entry of this.uploader.queue) {
            psum += entry.progress;
        }

        if (psum == 0)
            return 0;

        return Math.round(psum / this.uploader.queue.length);
    };
    public message: string;
    public uploader: Uploader = new Uploader();
    public centralRegistry: CentralRegistry;

    constructor(private http: HttpClient) {
        this.message = '';
    }

    onFilesChange(fileList: Array<File>) {
        for (let file of fileList) {
            this.uploader.queue.push(new UploadQueue(file));
        };
    }

    public ngOnInit() {
        
        this.liveOnInit();
        
    }


    onFileInvalids(fileList: Array<File>) {
        //TODO handle invalid files here  
    }

    onSelectChange(event: EventTarget) {
        let eventObj: MSInputMethodContext = <MSInputMethodContext>event;
        let target: HTMLInputElement = <HTMLInputElement>eventObj.target;
        let files: FileList = target.files;
        let file = files[0];
        if (file) {
            this.uploader.queue.push(new UploadQueue(file));
            //console.log(file);  
            console.log('Total Count:' + this.uploader.queue.length);
        }

    }

    private liveOnInit() {
        this._hubConnection = new HubConnectionBuilder()
            .withUrl(
                `${window.location.protocol}//${document.location.hostname}:${environment.port}/ProgressHub`
            )
            .configureLogging(LogLevel.Error)
            .build();
        this._hubConnection.serverTimeoutInMilliseconds = 120000;

        this._hubConnection.start().catch(err => console.error(err.toString()));

       

        this._hubConnection.on("ReceiveProgress", (progress: number) => {
            this.progresss = progress;
            this.progressSubject.next(progress);
        });

       
       


       

       

    }
   

    private getCurrrentProgress(extract: string, progress: string) {
        let overallProgress = 0;
        const ecount = this.extracts.length;
        const keys = this.extracts.map(x => `CT-${x.name}`);
        const key = `CT-${extract}`;
        localStorage.setItem(key, progress);
        keys.forEach(k => {
            const data = localStorage.getItem(k);
            if (data) {
                overallProgress = overallProgress + (+data);
            }
        });
        if (this.smartMode) {
            return overallProgress / ecount;
        }
        return overallProgress;
    }
    private updateExractStats(dwhProgress: any) {
        if (dwhProgress) {
            this.extracts.map(e => {
                if (e.name === dwhProgress.extract && e.extractEvent) {
                    e.extractEvent.sent = dwhProgress.sent;
                }
            }
            );
        }
    }


    // upload   
    upload(id) {
        if (id == null)
            return;

        let selectedFile = this.uploader.queue.find(s => s.id == id);
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];        
        if (selectedFile) {
            const formData = new FormData();
            formData.append(selectedFile.file.name, selectedFile.file);

            const uploadReq = new HttpRequest('POST', `api/upload`, formData, {
                reportProgress: true,
                
            });

            this.http.request(uploadReq).subscribe(
                p => {
                    // this.sendResponse = p;
                },            
                e => {
                    this.notifications = [];
                    console.error('SEND ERROR', e);
                    if (e && e.ProgressEvent) {

                    } else {
                        this.errorMessage = [];
                        this.errorMessage.push({ severity: 'error', summary: 'Error sending ', detail: <any>e });
                    }                    
                },
                () => {
                    this.notifications = [];
                    this.errorMessage.push({ severity: 'success', summary: 'Sending Extracts Completed ' });
                    this.sending = false;
                }
            );

        }
    }
    //upload all selected files to server  
    uploadAll() {
        //find the remaning files to upload  
        let remainingFiles = this.uploader.queue.filter(s => !s.isSuccess);
        for (let item of remainingFiles) {
            this.upload(item.id);
        }
    }

    // cancel all   
 
}



