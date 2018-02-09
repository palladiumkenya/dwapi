import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {CentralRegistry} from '../model/central-registry';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class EmrConfigService {

    private _url: string = './api/RegistryManager';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }
r
    public getAll(): Observable<CentralRegistry> {
        return this._http.get<CentralRegistry>(this._url + '/default')
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }
}
