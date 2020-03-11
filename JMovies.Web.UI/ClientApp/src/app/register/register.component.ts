import { Component, OnInit } from '@angular/core';
import { RegisterRequest } from '../models/request-models/registerRequest';
import { RegisterResponse } from '../models/response-models/registerResponse';
import { HttpService } from '../http.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.less']
})
export class RegisterComponent implements OnInit {

    constructor(public httpService: HttpService, public router: Router) { }

    ngOnInit(): void {
    }

    public register(): void {
        var vm = this;

        this.httpService.postToAction<RegisterResponse>("Register", this.request).subscribe((response) => {
            this.router.navigate(["/register-complete"]);
        });
    }

    public request: RegisterRequest = {};
}
