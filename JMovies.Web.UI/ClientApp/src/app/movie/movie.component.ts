import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { GetMovieDetailsResponse } from '../models/response-models/getMovieDetailsResponse';
import { Movie } from '../models/general-models/movie';
import * as _ from 'lodash';
import { MovieService } from '../movie.service';
import { ActingCredit } from '../models/general-models/acting-credit';
import { JM } from 'jm-utilities';
import { CreditRoleType } from '../models/general-models/credit-role-type-enum';

@Component({
    selector: 'app-movie',
    templateUrl: './movie.component.html',
    styleUrls: ['./movie.component.less']
})
export class MovieComponent implements OnInit, OnDestroy {

    constructor(
        public route: ActivatedRoute,
        public router: Router,
        public movieService: MovieService) {
        this.boundResizeFunction = this.onResize.bind(this);
    }

    ngOnInit() {
        const vm = this;
        this.route.paramMap.pipe(
            switchMap((params: ParamMap) =>
                vm.movieService.getMovieDetails(params.get('id')))
        ).subscribe((movieDetailsResponse: GetMovieDetailsResponse) => {
            vm.movie = movieDetailsResponse.movie;
            if (JM.isDefined(vm.movie)) {
                let summaryCount = 0;
                vm.summaryActingCredits = _.filter(vm.movie.credits, (credit: ActingCredit): boolean => {
                    if (credit.roleType == CreditRoleType.Acting && summaryCount < 5) {
                        summaryCount++;
                        return true;
                    } else {
                        return false;
                    }
                });
            }
            $(window).on("resize", vm.boundResizeFunction);
            _.defer(function () {
                vm.checkScrollElement();
            });
        }, (error) => {
            console.error(error);
        });
    }

    ngOnDestroy(): void {
        $(window).off("resize", this.boundResizeFunction);
        $(".main-actors>.slimScrollDiv").slimScroll({ destroy: true });
    }

    private onResize() {
        this.checkScrollElement();
    }
    private boundResizeFunction: any;

    private checkScrollElement(): void {
        var self = this;
        if ($(window).width() < 768) {
            $(".main-actors>.slimScrollDiv").slimScroll({ destroy: true });
        } else if ($(".main-actors>.flex-column").length == 0) {
            $(".main-actors>.flex-column").slimScroll();
        }
    }

    public movie: Movie;
    public summaryActingCredits: ActingCredit[];
    public currentPage = "movie";
}
