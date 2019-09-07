import { Component, OnInit, Input } from '@angular/core';
import { Credit } from '../models/general-models/credit';

@Component({
  selector: 'app-production-credits',
  templateUrl: './production-credits.component.html',
  styleUrls: ['./production-credits.component.less'],
    inputs: ['credits']
})
export class ProductionCreditsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

    @Input() credits: Credit[];
}
