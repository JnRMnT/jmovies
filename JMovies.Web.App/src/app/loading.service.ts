import { Injectable, ChangeDetectorRef, Optional } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class LoadingService {
    public active: boolean = false;
    constructor(@Optional() private cdRef: ChangeDetectorRef) {
    }

    public activeLoading(): void {
        this.activeCalls++;
        this.active = true;
        if (this.cdRef) {
            this.cdRef.detectChanges();
        }
    }

    public attemptToDeactivate(): void {
        this.activeCalls--;
        if (this.activeCalls <= 0) {
            this.active = false;
            if (this.cdRef) {
                this.cdRef.detectChanges();
            }
        }
    }

    private activeCalls: number = 0;
}