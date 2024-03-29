import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Extract} from '../../settings/model/extract';
import {DatabaseProtocol} from '../../settings/model/database-protocol';
import {Observable} from 'rxjs/Observable';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';
import {ExtractEvent} from '../../settings/model/extract-event';
import { LoadFromEmrCommand } from '../../settings/model/load-from-emr-command';
import {ExtractPatient} from '../ndwh-docket/model/extract-patient';
import { LoadExtracts } from '../../settings/model/load-extracts';

@Injectable()
export class NdwhExtractService {

  private _url: string = './api/DwhExtracts';
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
    public extract(extract: ExtractPatient): Observable<boolean> {
        return this._http.post<boolean>(this._url + '/extract', extract)
            .catch(this.handleError);
    }

    public extractAll(loadExtracts: LoadExtracts): Observable<boolean> {
        return this._http.post<boolean>(this._url + '/extractAll', loadExtracts)
            .catch(this.handleError);
    }

    public send(extracts: ExtractDatabaseProtocol[]): Observable<boolean> {
        return this._http.post<boolean>(this._url + '/load', extracts)
            .catch(this.handleError);
    }

    public loadMet(): Observable<any> {
        return this._http.get<any>( './api/Mts/load')
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }

}
