import { Injectable } from '@angular/core';
import * as _ from 'lodash';

@Injectable({
    providedIn: 'root'
})
export class LoadingService {
    public active: boolean = false;
    constructor() {
    }

    public activeLoading(): void {
        var self = this;
        this.activeCalls++;
        _.defer(function () {
            self.active = true;
        });
    }

    public attemptToDeactivate(): void {
        var self = this;
        this.activeCalls--;
        if (this.activeCalls <= 0) {
            this.active = false;
            _.defer(function () {
                self.active = false;
            });
        }
    }

    private activeCalls: number = 0;
}