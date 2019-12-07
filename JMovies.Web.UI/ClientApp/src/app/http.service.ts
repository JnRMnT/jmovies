import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { LoadingService } from './loading.service';
import { Observable, Observer } from 'rxjs';
import { JM } from 'jm-utilities';

@Injectable({
    providedIn: 'root'
})
export class HttpService {
    constructor(private http: HttpClient, private loadingService: LoadingService) {

    }

    get<T>(url: string): Observable<T> {
        const me = this;
        return Observable.create(observer => {
            me.loadingService.activeLoading();
            me.http.get<T>(this.getApiUrl(url), me.getHttpConfig()).subscribe(data => {
                me.loadingService.attemptToDeactivate();
                if (!me.checkAndHandleErrors(data, observer)) {
                    observer.next(data);
                    observer.complete();
                }
            }, error => {
                me.loadingService.attemptToDeactivate();
                observer.error(error);
            });
        });
    }

    post<T>(url: string, requestObject: any): Observable<T> {
        const me = this;
        return Observable.create(observer => {
            me.loadingService.activeLoading();
            me.http.post<T>(this.getApiUrl(url), requestObject, me.getHttpConfig()).subscribe(data => {
                me.loadingService.attemptToDeactivate();
                if (!me.checkAndHandleErrors(data, observer)) {
                    observer.next(data);
                    observer.complete();
                }
            }, error => {
                me.loadingService.attemptToDeactivate();
                observer.error(error);
            });
        });
    }

    getApiUrl(url: string): string {
        if (url.substr(0, 4) == "http") {
            return url;
        } else {
            return "api/" + url;
        }
    }

    getHttpConfig(): any {
        var headers: HttpHeaders = new HttpHeaders();
        return {
            headers: headers
        };
    }

    checkAndHandleErrors(data, observer: Observer<any>): boolean {
        if (JM.isDefined(data) && data.isSuccess == false) {
            observer.error(data.errors);
            return true;
        } else {
            return false;
        }
    }
}
