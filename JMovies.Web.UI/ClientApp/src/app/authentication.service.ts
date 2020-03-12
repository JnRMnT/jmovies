import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from '@angular/core';
import { JM } from 'jm-utilities';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    constructor(public jwtHelper: JwtHelperService) { }

    public isAuthenticated(): boolean {
        // Check whether the token is expired and return true or false
        return JM.isDefined(window.localStorage.getItem("jmAuthToken"))
            && !this.jwtHelper.isTokenExpired(window.localStorage.getItem("jmAuthToken"));
    }

    public setAuthenticated(token: string): void {
        window.localStorage.setItem("jmAuthToken", token);
    }
}
