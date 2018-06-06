import { Injectable } from '@angular/core';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {ExtractPatient} from '../ndwh-docket/model/extract-patient';
import {LoadFromEmrCommand} from '../../settings/model/load-from-emr-command';
import {ExtractEvent} from '../../settings/model/extract-event';
import {MasterPatientIndex} from '../models/master-patient-index';

@Injectable()
export class CbsService {

    private _url: string = './api/Cbs';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getStatus(extractId: string): Observable<ExtractEvent> {
        return this._http.get<ExtractEvent>(this._url + '/status/' + extractId)
            .catch(this.handleError);
    }

    public extract(extract: ExtractPatient): Observable<boolean> {
        console.log(extract);
        return this._http.post<boolean>(this._url + '/extract', extract)
            .catch(this.handleError);
    }

    public getDetailCount(): Observable<number> {
        return this._http.get<number>(this._url + '/count')
            .catch(this.handleError);
    }
    public getDetails(): Observable<MasterPatientIndex[]> {
        return this._http.get<MasterPatientIndex[]>(this._url)
            .catch(this.handleError);
    }

    public send(extracts: ExtractDatabaseProtocol[]): Observable<boolean> {
        return this._http.post<boolean>(this._url + '/load', extracts)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }


}
