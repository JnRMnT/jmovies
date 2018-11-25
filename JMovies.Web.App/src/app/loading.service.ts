import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class LoadingService {
    public active: boolean = false;
    constructor() {
    }

    public activeLoading(): void {
        this.activeCalls++;
        this.active = true;
    }

    public attemptToDeactivate(): void {
        this.activeCalls--;
        if (this.activeCalls <= 0) {
            this.active = false;
        }
    }

    private activeCalls: number = 0;
}