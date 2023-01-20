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

    public extracts: Extract[] = [];
    public errorMessage: Message[];
    public currentExtract: Extract;
    private extractEvent: ExtractEvent;

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
                `${window.location.protocol}//${document.location.hostname}:${environment.port}/ExtractActivity`
            )
            .configureLogging(LogLevel.Error)
            .build();
        this._hubConnection.serverTimeoutInMilliseconds = 120000;

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('ShowProgress', (extractActivityNotification: any) => {
            this.currentExtract = this.extracts.find(
                x => x.id === extractActivityNotification.extractId
            );
            if (this.currentExtract) {
                this.extractEvent = {
                    lastStatus: `${extractActivityNotification.progress.status}`,
                    found: extractActivityNotification.progress.found,
                    loaded: extractActivityNotification.progress.loaded,
                    rejected: extractActivityNotification.progress.rejected,
                    queued: extractActivityNotification.progress.queued,
                    sent: extractActivityNotification.progress.sent
                };
                this.currentExtract.extractEvent = {};
                this.currentExtract.extractEvent = this.extractEvent;
                const newWithoutPatientExtract = this.extracts.filter(
                    x => x.id !== extractActivityNotification.extractId
                );
                this.extracts = [
                    ...newWithoutPatientExtract,
                    this.currentExtract
                ];
            }
        });

        this._hubConnection.on('ShowDwhSendProgress', (dwhProgress: any) => {
            const progress = this.getCurrrentProgress(dwhProgress.extract, dwhProgress.progress);
            this.sendEvent = {
                sentProgress: progress
            };
            this.updateExractStats(dwhProgress);
            if (progress !== 100) {
                this.sending = true;
            } else {
                this.sending = false;
                
            }
            
        });
      


        this._hubConnection.on('ShowDwhSendMessage', (message: any) => {
            if (message === 'Sending started...') {
                localStorage.clear();
            }
            if (message.error) {
                this.errorMessage.push({ severity: 'error', summary: 'Error sending ', detail: message.message });
            } else {
                this.errorMessage.push({ severity: 'success', summary: message.message });
            }
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
       
        
        if (selectedFile) {
            const formData = new FormData();
            formData.append(selectedFile.file.name, selectedFile.file);

            const uploadReq = new HttpRequest('POST', `api/upload`, formData, {
                reportProgress: true,
            });

            this.http.request(uploadReq).subscribe(event => {
                if (event.type === HttpEventType.UploadProgress) {
                    selectedFile.progress = Math.round(100 * event.loaded / event.total);
                    this.sending = true;
                }
                else if (event.type === HttpEventType.Response)
                    selectedFile.message = event.body.toString();
            });
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
    cancelAll() {
        //TODO  
    }
}



