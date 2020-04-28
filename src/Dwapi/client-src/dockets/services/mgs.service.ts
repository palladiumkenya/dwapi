import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {ExtractEvent} from '../../settings/model/extract-event';
import {LoadFromEmrCommand} from '../../settings/model/load-from-emr-command';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';
import {LoadMgsExtracts} from '../../settings/model/load-mgs-extracts';

@Injectable()
export class MgsService {

    private _url: string = './api/Mgs';
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

    public extractAll(loadExtracts: LoadMgsExtracts): Observable<boolean> {
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
