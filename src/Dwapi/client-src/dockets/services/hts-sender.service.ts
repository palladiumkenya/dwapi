import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {CombinedPackage} from '../../settings/model/combined-package';
import {Observable} from 'rxjs/Observable';
import {SendResponse} from '../../settings/model/send-response';
import {SendPackage} from '../../settings/model/send-package';

@Injectable()
export class HtsSenderService {

    private _url: string = './api/Hts';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }


    public sendManifest(sendPackage: SendPackage): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/manifest`, sendPackage)
            .catch(this.handleError);
    }

    public sendClientExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/clients`, sendPackage)
            .catch(this.handleError);
    }
    public sendClientLinkageExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/linkages`, sendPackage)
            .catch(this.handleError);
    }
    public sendClientPartnerExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/partners`, sendPackage)
            .catch(this.handleError);
    }

    public sendClientsExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/clients`, sendPackage)
            .catch(this.handleError);
    }

    public sendClientTestsExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/clienttests`, sendPackage)
            .catch(this.handleError);
    }

    public sendClientsLinkageExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/clientslinkage`, sendPackage)
            .catch(this.handleError);
    }

    public sendTestKitsExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/testkits`, sendPackage)
            .catch(this.handleError);
    }

    public sendClientTracingExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/clienttracing`, sendPackage)
            .catch(this.handleError);
    }

    public sendPartnerTracingExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/partnertracing`, sendPackage)
            .catch(this.handleError);
    }

    public sendPartnerNotificationServicesExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/partnernotificationservices`, sendPackage)
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
