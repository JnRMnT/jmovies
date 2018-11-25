import * as $ from 'jquery';
import { Component } from '@angular/core';
import { HttpService } from './http.service';
import { LoadingService } from './loading.service';
import { TranslateService } from './translate.service';
import { ViewContainerRef, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements AfterViewInit {
    constructor(httpService: HttpService,
        private loadingService: LoadingService,
        vcr: ViewContainerRef,
        public translateService: TranslateService) {
        loadingService.activeLoading();
        this.renderedCulture = translateService.activeCulture;
    }
    title = 'JMovies';

    public handleCultureChange(cultureCode): void {
        const me = this;
        this.loadingService.activeLoading();
        setTimeout(() => {
            me.renderedCulture = cultureCode;
            me.loadingService.attemptToDeactivate();
        });
    }

    ngAfterViewInit() {
        this.loadingService.attemptToDeactivate();
    }

    public renderedCulture: string;
}
