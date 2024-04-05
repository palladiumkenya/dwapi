import { Injectable } from '@angular/core';
import { ExtractDatabaseProtocol } from '../../settings/model/extract-protocol';
import { HttpClient, HttpErrorResponse, HttpEvent, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { ExtractPatient } from '../ndwh-docket/model/extract-patient';
import { LoadFromEmrCommand } from '../../settings/model/load-from-emr-command';
import { ExtractEvent } from '../../settings/model/extract-event';
import { ClientRegistryExtract } from '../models/client-registry-extract';
import { SendResponse } from '../../settings/model/send-response';
import { SendPackage } from '../../settings/model/send-package';
import { UploadPackage } from '../../settings/model/upload-package';
import { CentralRegistry } from '../../settings/model/central-registry';
import { TotalClients } from "../models/totalClients";

@Injectable()
export class UploadService {

    private _url: string = './api/Upload';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getStatus(extractId: string): Observable<ExtractEvent> {
        return this._http.get<ExtractEvent>(this._url + '/status/' + extractId)
            .catch(this.handleError);
    }

    public extract(extract: ExtractPatient): Observable<boolean> {
        //console.log(extract);
        return this._http.post<boolean>(this._url + '/extract', extract)
            .catch(this.handleError);
    }

    public getDetailCount(): Observable<number> {
        return this._http.get<number>(this._url + '/count')
            .catch(this.handleError);
    }
    public getDetails(): Observable<ClientRegistryExtract[]> {
        return this._http.get<ClientRegistryExtract[]>(this._url)
            .catch(this.handleError);
    }

    public getAllDetailCount(): Observable<number> {
        return this._http.get<number>(this._url + '/allcount')
            .catch(this.handleError);
    }

    public getCrsExtractSummery(): Observable<TotalClients> {
        return this._http.get<TotalClients>(this._url + '/crssummary')
            .catch(this.handleError);
    }

    public getAllDetails(): Observable<ClientRegistryExtract[]> {
        return this._http.get<ClientRegistryExtract[]>(this._url + '/all')
            .catch(this.handleError);
    }

    public sendManifest(sendPackage: SendPackage): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/manifest`, sendPackage)
            .catch(this.handleError);
    }

    public sendCrs(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/crs`, sendPackage)
            .catch(this.handleError);
    }

    public exportCrs(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/crsExport`, sendPackage)
            .catch(this.handleError);
    }

    public exportManifest(sendPackage: SendPackage): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/manifestExport`, sendPackage)
            .catch(this.handleError);
    }
    public getExportedFiles(): Observable<string[]> {
        return this._http.get<string[]>(this._url + '/displayFiles')
            .catch(this.handleError);
    }

    public getFiles(): Observable<string[]> {
        return this._http.get<string[]>(this._url + '/files');
    }

    public downloadFile(file: string): Observable<HttpEvent<Blob>> {
        return this._http.request(new HttpRequest(
            'GET',
            `${this._url + '/download'}?file=${file}`,
            null,
            {
                reportProgress: true,
                responseType: 'blob'
            }));
    }

    public uploadFile(file: Blob): Observable<HttpEvent<void>> {
        const formData = new FormData();
        formData.append('file', file);

        return this._http.request(new HttpRequest(
            'POST',
            `${this._url}/sendfiles`,
            formData,
            {
                reportProgress: true
            }));
    }
    public UploadManifest(sendPackage: UploadPackage): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/manifest`, sendPackage)
            .catch(this.handleError);
    }



    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }


}
