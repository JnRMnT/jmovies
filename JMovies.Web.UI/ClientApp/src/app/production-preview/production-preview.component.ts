import { Component, OnInit, Input } from '@angular/core';
import { Production } from '../models/general-models/production';

@Component({
    selector: 'app-production-preview',
    templateUrl: './production-preview.component.html',
    styleUrls: ['./production-preview.component.less']
})
export class ProductionPreviewComponent implements OnInit {

    constructor() { }

    ngOnInit() {
    }

    @Input() production: Production;
}
