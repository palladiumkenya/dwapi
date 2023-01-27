import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {CombinedPackage} from '../../settings/model/combined-package';
import {Observable} from 'rxjs/Observable';
import {SendResponse} from '../../settings/model/send-response';
import {SendPackage} from '../../settings/model/send-package';

@Injectable()
export class MnchSenderService {

    private _url: string = './api/Mnch';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public sendManifest(sendPackage: SendPackage): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/manifest`, sendPackage)
            .catch(this.handleError);
    }

    public sendPatientMnchExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/patientmnchs`, sendPackage)
            .catch(this.handleError);
    }
    public sendAncVisitExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/ancvisits`, sendPackage)
            .catch(this.handleError);
    }
    public sendCwcEnrolmentExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/cwcenrolments`, sendPackage)
            .catch(this.handleError);
    }

    public sendCwcVisitExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/cwcvisits`, sendPackage)
            .catch(this.handleError);
    }

    public sendHeiExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/heis`, sendPackage)
            .catch(this.handleError);
    }

    public sendMatVisitExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/matvisits`, sendPackage)
            .catch(this.handleError);
    }

    public sendMnchArtExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/mncharts`, sendPackage)
            .catch(this.handleError);
    }

    public sendMnchEnrolmentExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/mnchenrolments`, sendPackage)
            .catch(this.handleError);
    }

    public sendMnchLabExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/mnchlabs`, sendPackage)
            .catch(this.handleError);
    }

    public sendMotherBabyPairExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/motherbabypairs`, sendPackage)
            .catch(this.handleError);
    }

    public sendPncVisitExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/pncvisits`, sendPackage)
            .catch(this.handleError);
    }


    public exportManifest(sendPackage: SendPackage): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/exportmanifest`, sendPackage)
            .catch(this.handleError);
    }
    public exportPatientMnchExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportpatientmnchs`, sendPackage)
            .catch(this.handleError);
    }

    public exportAncVisitExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportancvisits`, sendPackage)
            .catch(this.handleError);
    }
    public exportCwcEnrolmentExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportcwcenrolments`, sendPackage)
            .catch(this.handleError);
    }

    public exportCwcVisitExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportcwcvisits`, sendPackage)
            .catch(this.handleError);
    }

    public exportHeiExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportheis`, sendPackage)
            .catch(this.handleError);
    }

    public exportMatVisitExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportmatvisits`, sendPackage)
            .catch(this.handleError);
    }

    public exportMnchArtExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportmncharts`, sendPackage)
            .catch(this.handleError);
    }

    public exportMnchEnrolmentExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportmnchenrolments`, sendPackage)
            .catch(this.handleError);
    }

    public exportMnchLabExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportmnchlabs`, sendPackage)
            .catch(this.handleError);
    }

    public exportMotherBabyPairExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportmotherbabypairs`, sendPackage)
            .catch(this.handleError);
    }

    public exportPncVisitExtracts(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportpncvisits`, sendPackage)
            .catch(this.handleError);
    }

    public sendHandshake(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/endsession`, sendPackage)
            .catch(this.handleError);
    }


    public zipMnchFiles(sendPackage: SendPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/zipfiles`, sendPackage)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }
}
