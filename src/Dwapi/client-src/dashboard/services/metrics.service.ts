import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {AppMetric} from '../models/app-metric';
import {AppCheck} from "../models/app-check";

@Injectable()
export class MetricsService {

    private _url: string = './api/Dashboard';
    private _checkUrl: string = './api/mts/Summary';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getMetrics(): Observable<AppMetric[]> {
        return this._http.get<AppMetric[]>(this._url )
            .catch(this.handleError);
    }

    public getChecks(): Observable<AppCheck[]> {
        return this._http.get<AppCheck[]>(this._checkUrl )
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('not found');
        }
        return Observable.throw(err.error);
    }

}
