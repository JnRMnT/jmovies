import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-movie-navigation',
  templateUrl: './movie-navigation.component.html',
    styleUrls: ['./movie-navigation.component.less'],
    inputs: ['current-page','movie-id']
})
export class MovieNavigationComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

    @Input("current-page") currentPage: string;
    @Input("movie-id") movieID: string;
}
