import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Extract} from '../../settings/model/extract';
import {DatabaseProtocol} from '../../settings/model/database-protocol';
import {Observable} from 'rxjs/Observable';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';

@Injectable()
export class PsmartExtractService {

    private _url: string = './api/ExtractLoader';
    private _http: HttpClient;
    private _extractDatabaseProtocol: ExtractDatabaseProtocol;

    public constructor(http: HttpClient) {
        this._http = http;
    }


    public getStatus(extract: Extract, databaseProtocol: DatabaseProtocol): Observable<boolean> {
        this._extractDatabaseProtocol = {
            extract: extract,
            databaseProtocol: databaseProtocol
        };
        return this._http.post<boolean>(this._url + '/verify', this._extractDatabaseProtocol)
            .catch(this.handleError);
    }

    public load(extract: Extract, databaseProtocol: DatabaseProtocol): Observable<boolean> {
        this._extractDatabaseProtocol = {
            extract: extract,
            databaseProtocol: databaseProtocol
        };
        return this._http.post<boolean>(this._url + '/verify', this._extractDatabaseProtocol)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }
}
