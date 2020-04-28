import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {PageModel} from '../models/page-model';

@Injectable()
export class NdwhPatientPharmacyService {

  private _url: string = './api/PatientPharmacy';
  private _http: HttpClient;

  constructor(http: HttpClient) {
      this._http = http;
  }

    public loadValidCount(): Observable<number> {
        return this._http.get<any>(this._url + `/ValidCount`)
            .catch(this.handleError);
    }

    public loadValid(pageModel: PageModel): Observable<any[]> {
        return this._http.get<any>(this._url + `/loadValid/${pageModel.page}/${pageModel.pageSize}`)
            .catch(this.handleError);
    }

  public loadErrors(): Observable<any[]> {
      return this._http.get<any>(this._url + '/loadErrors')
          .catch(this.handleError);
  }

  public loadValidations(): Observable<any[]> {
    return this._http.get<any>(this._url + '/LoadValidations')
        .catch(this.handleError);
    }

  private handleError(err: HttpErrorResponse) {
      if (err.status === 404) {
          return Observable.throw('no record(s) found');
      }
      return Observable.throw(err.error);
  }
}
