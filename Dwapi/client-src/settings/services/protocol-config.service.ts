import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {CentralRegistry} from '../model/central-registry';
import {Observable} from 'rxjs/Observable';
import {EmrSystem} from '../model/emr-system';
import {DatabaseProtocol} from '../model/database-protocol';

@Injectable()
export class ProtocolConfigService {

    private _url: string = './api/EmrManager';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public saveProtocol(entity: DatabaseProtocol): Observable<DatabaseProtocol> {
        return this._http.post<DatabaseProtocol>(this._url + '/protocol', entity)
            .catch(this.handleError);
    }

    public verifyProtocol(entity: DatabaseProtocol): Observable<boolean> {
        return this._http.post<boolean>(this._url + '/verify', entity)
            .catch(this.handleError);
    }

    public makeEmrDefault(emr: EmrSystem): Observable<boolean> {
        return this._http.post<boolean>(this._url + '/setDefault', emr)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }

}
