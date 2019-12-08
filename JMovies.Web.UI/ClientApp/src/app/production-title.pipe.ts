import { Pipe, PipeTransform } from '@angular/core';
import { Production } from './models/general-models/production';
import { JM } from 'jm-utilities';
import { TVSeries } from './models/general-models/tv-series';

@Pipe({
  name: 'productionTitle'
})
export class ProductionTitlePipe implements PipeTransform {

    transform(production: Production, ...args: any[]): any {
        let title = "";
        if (JM.isDefined(production)) {
            title += production.title;
            if (JM.isDefined(production.year)) {
                title += " (" + production.year;
                if (JM.isDefined((<TVSeries>production).endYear)) {
                    title += " - " + (<TVSeries>production).endYear;
                }
                title += ")";
            }
        }
        return title;
  }
}
