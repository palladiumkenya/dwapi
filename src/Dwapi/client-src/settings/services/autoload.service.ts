import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {CentralRegistry} from '../model/central-registry';
import {Observable} from 'rxjs/Observable';
// import {EmrSystem} from '../model/emr-system';
import {DatabaseProtocol} from '../model/database-protocol';
import {EmrMetrics} from '../model/emr-metrics';

@Injectable()
export class AutoloadService {

    private _url: string = './api/RefreshETL';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public refreshETL(): Observable<string> {
        return this._http.get<string>(this._url)
            .catch(this.handleError);
    }
    // public getCount(): Observable<number> {
    //     return this._http.get<number>(this._url + '/count')
    //         .catch(this.handleError);
    // }
    // public save(entity: EmrSystem): Observable<EmrSystem> {
    //     return this._http.post<EmrSystem>(this._url, entity)
    //         .catch(this.handleError);
    // }
    //
    // public makeEmrDefault(emr: EmrSystem): Observable<boolean> {
    //     return this._http.post<boolean>(this._url + '/setDefault', emr)
    //         .catch(this.handleError);
    // }
    // public delete(id: string): Observable<number> {
    //     return this._http.delete<number>(this._url + '/' + id)
    //         .catch(this.handleError);
    // }
    //
    // public getDefault(): Observable<EmrSystem> {
    //     return this._http.get<EmrSystem>(this._url + '/default')
    //         .catch(this.handleError);
    // }
    //
    // public getMiddleware(): Observable<EmrSystem> {
    //     return this._http.get<EmrSystem>(this._url + '/middleware')
    //         .catch(this.handleError);
    // }
    //
    // public loadMetrics(emr: EmrSystem): Observable<EmrMetrics> {
    //     return this._http.post<boolean>(this._url + '/metrics', emr)
    //         .catch(this.handleError);
    // }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }
}
