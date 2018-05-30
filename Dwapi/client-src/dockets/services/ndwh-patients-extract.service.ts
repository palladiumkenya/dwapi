import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { PatientExtract } from '../models/patient-extract';

@Injectable()
export class NdwhPatientsExtractService {
    private _url: string = './api/Patients';
    private _http: HttpClient;

    constructor(http: HttpClient) {
        this._http = http;
    }

    public loadValid(): Observable<PatientExtract[]> {
        return this._http.get<PatientExtract>(this._url + '/loadValid')
            .catch(this.handleError);
    }

    public loadErrors(): Observable<PatientExtract[]> {
        return this._http.get<PatientExtract>(this._url + '/loadErrors')
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }
}
