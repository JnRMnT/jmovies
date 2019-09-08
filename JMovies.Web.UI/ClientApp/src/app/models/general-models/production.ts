import { ProductionTypeEnum } from "./production-type-enum";
import { TVSeries } from './tv-series';
import { Movie } from './movie';
import { Image } from './image';

export class BaseProduction {
  imdbLink: string;
  imDbID: number;
  title: string;
  year: number;
  productionType: ProductionTypeEnum;
  poster: Image;
} 

export type Production = TVSeries | Movie | BaseProduction;
