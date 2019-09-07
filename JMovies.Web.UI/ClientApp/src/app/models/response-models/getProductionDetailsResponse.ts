import { Movie } from "../general-models/movie";
import { Production } from '../general-models/production';
import { TVSeries } from '../general-models/tv-series';

export class GetProductionDetailsResponse{
    production: Production | Movie | TVSeries;
}
