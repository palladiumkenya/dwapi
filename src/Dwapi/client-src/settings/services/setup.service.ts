import { Injectable } from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {AppDatabase} from '../model/app-database';

@Injectable()
export class SetupService {

    private _url = './api/wizard';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getDatabase(): Observable<AppDatabase> {
        return this._http.get<AppDatabase>(`${this._url}/db`)
            .catch(this.handleError);
    }

    public verifyServer(entity: AppDatabase): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/verifyserver`, entity)
            .catch(this.handleError);
    }

    public verifyDatabase(entity: AppDatabase): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/verifydb`, entity)
            .catch(this.handleError);
    }

    public saveDatabase(entity: AppDatabase): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/db`, entity)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('could not be found');
        }
        return Observable.throw(err.error);
    }

}
