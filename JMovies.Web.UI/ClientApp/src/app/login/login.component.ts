import { Component, OnInit } from '@angular/core';
import { LoginRequest } from '../models/request-models/loginRequest';
import { HttpService } from '../http.service';
import { LoginResponse } from '../models/response-models/loginResponse';
import { AuthenticationService } from '../authentication.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.less']
})
export class LoginComponent implements OnInit {

    constructor(public httpService: HttpService, public authenticationService: AuthenticationService) { }

    ngOnInit() {
    }

    login(): void {
        this.httpService.post<LoginResponse>("Login", this.request).subscribe((response) => {
            this.authenticationService.setAuthenticated(response.token);
            window.location.href = "/";
        });
    }

    public request: LoginRequest = {};
}
