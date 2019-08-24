import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
    selector: 'jm-button',
    templateUrl: './jm-button.component.html',
    styleUrls: ['./jm-button.component.less'],
    inputs: ['class', 'onClick', 'routerLink', 'buttonType']
})
export class JmButtonComponent implements OnInit, OnChanges {

    constructor() { }

    ngOnInit() {
        this.setInternalClass(this.class);
    }

    ngOnChanges(changes: SimpleChanges) {
        this.setInternalClass(changes.class.currentValue);
    }

    public onInternalClick($event: Event): void {
        if (this.onClick) {
            this.onClick($event);
        }
    }

    public setInternalClass(classValue: string): void {
        this.internalClass = "btn " + (this.buttonType ? this.buttonType : "btn-primary") + " " + classValue;
    }

    @Input() class: string;
    @Input() routerLink: string;
    @Input() onClick: Function;
    @Input() buttonType: string;
    public internalClass: string;
}
