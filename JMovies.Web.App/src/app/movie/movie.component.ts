import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HttpService } from '../http.service';
import { GetMovieDetailsResponse } from '../models/response-models/getMovieDetailsResponse';
import { Movie } from '../models/general-models/movie';
const JM = require("jm-utilities");

@Component({
    selector: 'app-movie',
    templateUrl: './movie.component.html',
    styleUrls: ['./movie.component.less']
})
export class MovieComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private httpService: HttpService) {

    }

    ngOnInit() {
        const vm = this;
        this.route.paramMap.pipe(
            switchMap((params: ParamMap) =>
                vm.getMovieDetails(params.get('id')))
        ).subscribe((movieDetailsResponse: GetMovieDetailsResponse) => {
            vm.movie = movieDetailsResponse.movie;
        }, (error) => {
            console.error(error);
        });
    }

    private getMovieDetails(id: string): Observable<any> {
        return this.httpService.get<GetMovieDetailsResponse>("movies/" + id);
    }

    private movie: Movie;
}
