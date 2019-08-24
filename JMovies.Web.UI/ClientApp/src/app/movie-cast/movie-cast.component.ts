import { Component, OnInit } from '@angular/core';
import { MovieService } from '../movie.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { GetMovieDetailsResponse } from '../models/response-models/getMovieDetailsResponse';
import { switchMap } from 'rxjs/operators';
import { Movie } from '../models/general-models/movie';

@Component({
  selector: 'app-movie-cast',
  templateUrl: './movie-cast.component.html',
  styleUrls: ['./movie-cast.component.less']
})
export class MovieCastComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private movieService: MovieService) { }

    ngOnInit() {
        const vm = this;
        this.route.paramMap.pipe(
            switchMap((params: ParamMap) =>
                vm.movieService.getMovieDetails(params.get('id')))
        ).subscribe((movieDetailsResponse: GetMovieDetailsResponse) => {
            vm.movie = movieDetailsResponse.movie;
        }, (error) => {
            console.error(error);
        });
    }

    public movie: Movie;
    public currentPage = "movie-cast";
}
