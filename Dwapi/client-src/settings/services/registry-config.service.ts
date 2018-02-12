import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {CentralRegistry} from '../model/central-registry';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import {VerificationResponse} from '../model/verification-response';


@Injectable()
export class RegistryConfigService {

    private _url: string = './api/RegistryManager';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getDefault(): Observable<CentralRegistry> {
        return this._http.get<CentralRegistry>(this._url + '/default')
            .catch(this.handleError);
    }

    public saveDefault(entity: CentralRegistry): Observable<CentralRegistry> {
        return this._http.post<CentralRegistry>(this._url, entity)
            .catch(this.handleError);
    }

    public verify(entity: CentralRegistry): Observable<VerificationResponse> {
        return this._http.post<VerificationResponse>(this._url + '/verify', entity)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }
}
