import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { JM } from 'jm-utilities';

@Component({
    selector: 'jm-button',
    templateUrl: './jm-button.component.html',
    styleUrls: ['./jm-button.component.less'],
    inputs: ['class', 'disabled', 'onClick', 'routerLink', 'buttonType', 'actionType']
})
export class JmButtonComponent implements OnInit, OnChanges {

    constructor() { }

    ngOnInit() {
        this.setInternalClass(this.class);
        this.setInternalActionType(this.internalActionType);
    }

    ngOnChanges(changes: SimpleChanges) {
        if (JM.isDefined(changes.class)) {
            this.setInternalClass(changes.class.currentValue);
        }
        if (JM.isDefined(changes.actionType)) {
            this.setInternalActionType(changes.actionType.currentValue);
        }
    }

    public onInternalClick($event: Event): void {
        if (this.onClick) {
            this.onClick($event);
        }
    }

    public setInternalActionType(actionType: string): void {
        this.internalActionType = JM.isDefined(actionType) ? actionType : "button";
    }

    public setInternalClass(classValue: string): void {
        this.internalClass = "btn " + (this.buttonType ? this.buttonType : "btn-primary") + " " + classValue;
    }

    @Input() class: string;
    @Input() disabled: string;
    @Input() routerLink: string;
    @Input() onClick: Function;
    @Input("button-type") buttonType: string;
    @Input("action-type") actionType: string;
    public internalClass: string;
    public internalActionType: string;
}
