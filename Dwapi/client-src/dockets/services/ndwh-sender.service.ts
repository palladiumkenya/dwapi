import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {SendResponse} from '../../settings/model/send-response';
import { CombinedPackage } from '../../settings/model/combined-package';

@Injectable()
export class NdwhSenderService {

  private _url: string = './api/DwhExtracts';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }


    public sendManifest(sendPackage: CombinedPackage): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/manifest`, sendPackage)
            .catch(this.handleError);
    }

    public sendPatientExtracts(sendPackage: CombinedPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/patients`, sendPackage)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }

}
