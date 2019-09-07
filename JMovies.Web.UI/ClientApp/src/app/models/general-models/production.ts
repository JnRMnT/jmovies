import { ProductionTypeEnum } from "./production-type-enum";
import { TVSeries } from './tv-series';
import { Movie } from './movie';

export class BaseProduction {
    imdbLink: string;
    imDbID: number;
    title: string;
    year: number;
    productonType: ProductionTypeEnum;
}

export type Production = TVSeries | Movie | BaseProduction;
