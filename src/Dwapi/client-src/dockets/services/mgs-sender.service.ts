import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {SendResponse} from '../../settings/model/send-response';
import {SendPackage} from '../../settings/model/send-package';

@Injectable()
export class MgsSenderService {

    private _url: string = './api/Mgs';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }


    public sendManifest(sendPackage: SendPackage): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/manifest`, sendPackage)
            .catch(this.handleError);
    }

    public sendMigrationExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/migrations`, sendPackage)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }
}
