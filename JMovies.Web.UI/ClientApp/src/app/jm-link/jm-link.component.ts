import { Component, OnInit, Input, HostBinding } from '@angular/core';

@Component({
    selector: 'jm-link',
    templateUrl: './jm-link.component.html',
    styleUrls: ['./jm-link.component.less'],
    inputs: ['url', 'onClick', 'target']
})
export class JmLinkComponent implements OnInit {
    constructor() { }

    ngOnInit() {
        if (!this.target) {
            this.target = "_self";
        }
    }

    public onInternalClick($event: Event): void {
        if (this.onClick) {
            this.onClick($event);
        }
    }

    @Input() url: string;
    @Input() target: string;
    @Input() onClick: Function;
    @HostBinding('class') class: string;
}
