import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {CombinedPackage} from '../../settings/model/combined-package';
import {Observable} from 'rxjs/Observable';
import {SendResponse} from '../../settings/model/send-response';
import {SendPackage} from '../../settings/model/send-package';

@Injectable()
export class PrepSenderService {

    private _url: string = './api/Prep';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public sendManifest(sendPackage: SendPackage): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/manifest`, sendPackage)
            .catch(this.handleError);
    }

    public sendPatientPrepExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/patientpreps`, sendPackage)
            .catch(this.handleError);
    }
    public sendPrepAdverseEventExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/PrepAdverseEvents`, sendPackage)
            .catch(this.handleError);
    }
    public sendPrepBehaviourRiskExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/PrepBehaviourRisks`, sendPackage)
            .catch(this.handleError);
    }

    public sendPrepCareTerminationExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/PrepCareTerminations`, sendPackage)
            .catch(this.handleError);
    }

    public sendPrepLabExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/preplabs`, sendPackage)
            .catch(this.handleError);
    }

    public sendPrepPharmacyExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/preppharmacys`, sendPackage)
            .catch(this.handleError);
    }

    public sendPrepVisitExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/prepvisits`, sendPackage)
            .catch(this.handleError);
    }

    public sendHandshake(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/endsession`, sendPackage)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }
}
