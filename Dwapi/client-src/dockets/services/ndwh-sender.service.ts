import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Extract} from '../../settings/model/extract';
import {DatabaseProtocol} from '../../settings/model/database-protocol';
import {Observable} from 'rxjs/Observable';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';
import {CentralRegistry} from '../../settings/model/central-registry';
import {SendResponse} from '../../settings/model/send-response';
import {SendPackage} from '../../settings/model/send-package';

@Injectable()
export class NdwhSenderService {

  private _url: string = './api/ExtractSender';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public send(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<SendResponse>(this._url, sendPackage)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }

}
