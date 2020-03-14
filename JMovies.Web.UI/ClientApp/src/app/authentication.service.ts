import { Injectable } from '@angular/core';
import { JM } from 'jm-utilities';
import * as moment from "moment";

@Injectable({ 
    providedIn: 'root'
})
export class AuthenticationService {

    constructor() { }

    public isAuthenticated(): boolean {
        // Check whether the token is expired and return true or false
        return JM.isDefined(window.localStorage.getItem("jmAuthToken"))
            && JM.isDefined(window.localStorage.getItem("jmAuthTokenIssueTime"))
            && moment(new Date(window.localStorage.getItem("jmAuthTokenIssueTime"))).add(15, "minutes").isAfter(moment());
    }

    public getToken(): string {
        return window.localStorage.getItem("jmAuthToken");
    }
    
    public setAuthenticated(token?: string): void {
        if (JM.isDefined(token)) {
            window.localStorage.setItem("jmAuthToken", token);
            window.localStorage.setItem("jmAuthTokenIssueTime", new Date().toString());
        } else {
            window.localStorage.removeItem("jmAuthToken");
            window.localStorage.removeItem("jmAuthTokenIssueTime");
        }
    }
}
