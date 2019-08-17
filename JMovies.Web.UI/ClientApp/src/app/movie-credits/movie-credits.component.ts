import { Component, OnInit, Input } from '@angular/core';
import { Credit } from '../models/general-models/credit';

@Component({
  selector: 'app-movie-credits',
  templateUrl: './movie-credits.component.html',
    styleUrls: ['./movie-credits.component.less'],
    inputs: ['credits']
})
export class MovieCreditsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

    @Input() credits: Credit[];
}
