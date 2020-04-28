import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AppDetailsService {

    private _url: string = './api/appDetails';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getVersion(): Observable<string> {
        return this._http.get<string>(this._url + '/version/')
            .catch(this.handleError);
    }
    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('not found');
        }
        return Observable.throw(err.error);
    }

}
