import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { JM } from 'jm-utilities';
import { GetMovieDetailsResponse } from './models/response-models/getMovieDetailsResponse';
import * as _ from 'lodash';

@Injectable({
    providedIn: 'root'
})
export class MovieService {

    constructor(private httpService: HttpService) {
        this.movieDetailsCache = {};
    }

    public getMovieDetails(id: string | number): Observable<any> {
        var self = this;
        return new Observable((observer) => {
            if (JM.isDefined(self.movieDetailsCache[id])) {
                observer.next(self.movieDetailsCache[id]);
            } else {
                this.httpService.get<GetMovieDetailsResponse>("movies/" + id).subscribe((response) => {
                    if (JM.isDefined(response.movie)) {
                        response.movie.imdbLink = "https://www.imdb.com/title/tt" + _.padStart(response.movie.imDbID.toString(), 7, '0');
                    }
                    self.movieDetailsCache[id] = response;
                    observer.next(response);
                }, (error) => {
                    observer.error(error);
                });
            }
        });
        return;
    }
    private movieDetailsCache;
}
