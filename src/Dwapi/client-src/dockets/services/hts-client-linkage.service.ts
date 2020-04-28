import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {PageModel} from '../models/page-model';

@Injectable()
export class HtsClientLinkageService {

    private _url: string = './api/HtsSummary';
    private _http: HttpClient;

    constructor(http: HttpClient) {
        this._http = http;
    }

    public loadValidCount(): Observable<number> {
        return this._http.get<any>(this._url + `/linkagecount`)
            .catch(this.handleError);
    }

    public loadValid(pageModel: PageModel): Observable<any[]> {
        return this._http.get<any>(this._url + `/linkage/${pageModel.page}/${pageModel.pageSize}`)
            .catch(this.handleError);
    }

    public loadValidations(): Observable<any[]> {
        return this._http.get<any>(this._url + '/linkagevalidations')
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }

}
