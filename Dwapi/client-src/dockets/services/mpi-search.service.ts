import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { MasterPatientIndex } from '../models/master-patient-index';
import {MpiSearch} from '../models/mpi-search';
import { SearchPackage } from '../models/mpi-search-package';

@Injectable()
export class MpiSearchService {

  private _url: string = './api/Cbs';
  private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public search(model: SearchPackage): Observable<MasterPatientIndex[]> {
        console.log(model);
      return this._http.post<MasterPatientIndex[]>(this._url + '/mpiSearch', model)
          .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
      if (err.status === 404) {
          return Observable.throw('no record(s) found');
      }
      return Observable.throw(err.error);
  }
}
