import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {CentralRegistry} from '../model/central-registry';
import {Observable} from 'rxjs/Observable';
import {EmrSystem} from '../model/emr-system';
import {DatabaseProtocol} from '../model/database-protocol';
import {Extract} from '../model/extract';
import {ExtractDatabaseProtocol} from '../model/extract-protocol';

@Injectable()
export class ExtractConfigService {

    private _url: string = './api/ExtractManager';
    private _http: HttpClient;
    private _extractDatabaseProtocol: ExtractDatabaseProtocol;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getAll(id: string, docketId: string ): Observable<Extract[]> {
        return this._http.get<Extract[]>(this._url + '/' + id + '/' + docketId)
            .catch(this.handleError);
    }

    public save(entity: Extract): Observable<Extract> {
        return this._http.post<Extract>(this._url, entity)
            .catch(this.handleError);
    }

    public verify(extract: Extract, databaseProtocol: DatabaseProtocol): Observable<boolean> {
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
