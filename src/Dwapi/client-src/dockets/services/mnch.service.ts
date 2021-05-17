import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {ExtractEvent} from '../../settings/model/extract-event';
import {ExtractPatient} from '../ndwh-docket/model/extract-patient';
import {MasterPatientIndex} from '../models/master-patient-index';
import {SendPackage} from '../../settings/model/send-package';
import {SendResponse} from '../../settings/model/send-response';
import {LoadFromEmrCommand} from '../../settings/model/load-from-emr-command';
import {LoadExtracts} from '../../settings/model/load-extracts';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';
import {LoadMnchExtracts} from '../../settings/model/load-mnch-extracts';

@Injectable()
export class MnchService {

    private _url: string = './api/Mnch';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getStatus(extractId: string): Observable<ExtractEvent> {
        return this._http.get<ExtractEvent>(this._url + '/status/' + extractId)
            .catch(this.handleError);
    }

    public load(extracts: LoadFromEmrCommand): Observable<boolean> {
        return this._http.post<boolean>(this._url + '/load', extracts)
            .catch(this.handleError);
    }

    public extractAll(loadExtracts: LoadMnchExtracts): Observable<boolean> {
        return this._http.post<boolean>(this._url + '/extractAll', loadExtracts)
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
