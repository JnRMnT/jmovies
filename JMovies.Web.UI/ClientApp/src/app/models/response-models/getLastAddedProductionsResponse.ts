import { BaseProduction, Production } from '../general-models/production';
import { Movie } from '../general-models/movie';
import { TVSeries } from '../general-models/tv-series';
import { ComplexCollection } from '../general-models/complexCollection';

export class GetLastAddedProductionsResponse {
    productions: ComplexCollection<Production>;
}
