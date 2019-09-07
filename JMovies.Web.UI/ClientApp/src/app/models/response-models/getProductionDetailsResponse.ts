import { Movie } from "../general-models/movie";
import { BaseProduction } from '../general-models/production';
import { TVSeries } from '../general-models/tv-series';

export class GetProductionDetailsResponse{
    production: BaseProduction | Movie | TVSeries;
}
