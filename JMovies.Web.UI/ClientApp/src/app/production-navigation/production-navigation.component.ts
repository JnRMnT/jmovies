import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-production-navigation',
  templateUrl: './production-navigation.component.html',
  styleUrls: ['./production-navigation.component.less'],
    inputs: ['current-page','production-id']
})
export class ProductionNavigationComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

    @Input("current-page") currentPage: string;
    @Input("production-id") productionID: string;
}
