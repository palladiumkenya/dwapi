import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {PageModel} from '../models/page-model';


@Injectable()
export class NdwhSummaryService {

  private _url: string = './api/';
  private _http: HttpClient;

  constructor(http: HttpClient) {
      this._http = http;
  }

    public loadValidCount(extract: string): Observable<number> {
        return this._http.get<any>(`${this._url}${extract}/ValidCount`)
            .catch(this.handleError);
    }

    public loadValid(extract: string,pageModel: PageModel): Observable<any[]> {
        return this._http.get<any>( `${this._url}${extract}/loadValid/${pageModel.page}/${pageModel.pageSize}`)
            .catch(this.handleError);
    }

  public loadErrors(extract: string): Observable<any[]> {
      return this._http.get<any>(`${this._url}${extract}/loadErrors`)
          .catch(this.handleError);
  }

  public loadValidations(extract: string): Observable<any[]> {
    return this._http.get<any>(`${this._url}${extract}/LoadValidations`)
        .catch(this.handleError);
    }

  private handleError(err: HttpErrorResponse) {
      if (err.status === 404) {
          return Observable.throw('no record(s) found');
      }
      return Observable.throw(err.error);
  }

}
