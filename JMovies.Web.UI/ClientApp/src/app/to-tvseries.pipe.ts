import { Pipe, PipeTransform } from '@angular/core';
import { TVSeries } from './models/general-models/tv-series';

@Pipe({
  name: 'toTVSeries'
})
export class ToTVSeriesPipe implements PipeTransform {

  transform(value: any, ...args: any[]): TVSeries {
    return <TVSeries>value;
  }

}
