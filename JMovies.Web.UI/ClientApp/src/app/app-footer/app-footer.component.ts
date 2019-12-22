import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';

@Component({
    selector: 'app-footer',
    templateUrl: './app-footer.component.html',
    styleUrls: ['./app-footer.component.less']
})
export class AppFooterComponent implements OnInit {
    copyRightText = "©" + moment().format("YYYY") + " JMovies";
    constructor() { }

    ngOnInit() {
    }
}
