import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.less']
})
export class AppHeaderComponent implements OnInit {

    constructor(public authenticationService: AuthenticationService) { }

  ngOnInit() {
  }

}
