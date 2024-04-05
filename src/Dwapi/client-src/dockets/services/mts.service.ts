import { Injectable } from '@angular/core';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';
import {HttpClient, HttpErrorResponse, HttpEvent, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {ExtractPatient} from '../ndwh-docket/model/extract-patient';
import {LoadFromEmrCommand} from '../../settings/model/load-from-emr-command';
import {ExtractEvent} from '../../settings/model/extract-event';
import {ClientRegistryExtract} from '../models/client-registry-extract';
import {SendResponse} from '../../settings/model/send-response';
import {SendPackage} from '../../settings/model/send-package';
import {CentralRegistry} from '../../settings/model/central-registry';
import {TotalClients} from "../models/totalClients";

@Injectable()
export class MtsService {

    private _url: string = './api/Mts';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getStatus(extractId: string): Observable<ExtractEvent> {
        return this._http.get<ExtractEvent>(this._url + '/status/' + extractId)
            .catch(this.handleError);
    }

    public Load(): Observable<boolean> {
        //console.log(extract);
        return this._http.get<boolean>(this._url + '/Load')
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

    public getMtsExtractSummery(): Observable<TotalClients> {
        return this._http.get<TotalClients>(this._url + '/mtssummary')
            .catch(this.handleError);
    }

    public getAllDetails(): Observable<ClientRegistryExtract[]> {
        return this._http.get<ClientRegistryExtract[]>(this._url + '/all')
            .catch(this.handleError);
    }
    //
    // public sendManifest(sendPackage: SendPackage): Observable<boolean> {
    //     return this._http.post<boolean>(`${this._url}/manifest`, sendPackage)
    //         .catch(this.handleError);
    // }

    public sendMts(metrics: any): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/sendMetrics`, metrics)
            .catch(this.handleError);
    }

    public verifyMtsApi(): Observable<SendResponse> {
        console.log(this._url + '/verify')
        return this._http.get(this._url + '/verify')
            .catch(this.handleError);
    }



    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }


}
