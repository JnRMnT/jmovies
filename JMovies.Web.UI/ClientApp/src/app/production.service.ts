import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { JM } from 'jm-utilities';
import { GetProductionDetailsResponse } from './models/response-models/getProductionDetailsResponse';
import * as _ from 'lodash';
import { Production } from './models/general-models/production';
import { Movie } from './models/general-models/movie';
import * as moment from "moment";

@Injectable({
  providedIn: 'root'
})
export class ProductionService {

  constructor(private httpService: HttpService) {
    this.movieDetailsCache = {};
  }

  public getProductionDetails(id: string | number): Observable<Production> {
    var self = this;
    return new Observable((observer) => {
      if (JM.isDefined(self.movieDetailsCache[id])) {
        observer.next(self.movieDetailsCache[id]);
      } else {
        this.httpService.get<Production>("production/" + id).subscribe((production) => {
          if (JM.isDefined(production)) {
            production.imdbLink = "https://www.imdb.com/title/tt" + _.padStart(production.imDbID.toString(), 8, '0');
          }
          var movie: Movie = <Movie>production;
          if (JM.isDefined(movie.runtime)) {
            movie.runtime = moment.utc(moment.duration(<string>movie.runtime).as('milliseconds')).format('H:mm');
          }
          self.movieDetailsCache[id] = production;
          observer.next(production);
        }, (error) => {
          observer.error(error);
        });
      }
    });
    return;
  }
  private movieDetailsCache;
}
