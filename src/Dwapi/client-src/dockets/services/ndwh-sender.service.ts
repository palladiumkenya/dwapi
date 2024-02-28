import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {SendResponse} from '../../settings/model/send-response';
import { CombinedPackage } from '../../settings/model/combined-package';
import {ManifestResponse} from "../models/manifest-response";

@Injectable()
export class NdwhSenderService {

  private _url: string = './api/DwhExtracts';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public checkWhichToSend(): Observable<ManifestResponse> {
        return this._http.get<boolean>(`${this._url}/checkWhichToSend`)
            .catch(this.handleError);
    }

    public sendManifest(sendPackage: CombinedPackage): Observable<ManifestResponse> {
        return this._http.post<boolean>(`${this._url}/manifest`, sendPackage)
            .catch(this.handleError);
    }
    public sendSmartManifest(sendPackage: CombinedPackage): Observable<ManifestResponse> {
        return this._http.post<boolean>(`${this._url}/smart/manifest`, sendPackage)
            .catch(this.handleError);
    }
    public sendDiffManifest(sendPackage: CombinedPackage): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/diffmanifest`, sendPackage)
            .catch(this.handleError);
    }

    public sendPatientExtracts(sendPackage: CombinedPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/patients`, sendPackage)
            .catch(this.handleError);
    }


    public sendSmartPatientExtracts(sendPackage: CombinedPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/smart/patients`, sendPackage)
            .catch(this.handleError);
    }

    public sendDiffPatientExtracts(sendPackage: CombinedPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/diffpatients`, sendPackage)
            .catch(this.handleError);
    }

    public exportManifest(sendPackage: CombinedPackage): Observable<ManifestResponse> {
        return this._http.post<boolean>(`${this._url}/exportmanifest`, sendPackage)
            .catch(this.handleError);
    }
    public exportSmartManifest(sendPackage: CombinedPackage): Observable<ManifestResponse> {
        return this._http.post<boolean>(`${this._url}/smart/exportmanifest`, sendPackage)
            .catch(this.handleError);
    }
    public exportPatientCTExtracts(sendPackage: CombinedPackage): Observable<SendResponse> {
        return this._http.post<boolean>(`${this._url}/exportpatientsCT`, sendPackage)
            .catch(this.handleError);
    }
    public exportSmartPatientExtracts(sendPackage: CombinedPackage): Observable<SendResponse> {
        console.log("sendPackage  ===> ",sendPackage)
        return this._http.post<boolean>(`${this._url}/smart/exportpatients`, sendPackage)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }

}
