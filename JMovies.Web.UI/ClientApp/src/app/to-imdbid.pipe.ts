import { Pipe, PipeTransform } from '@angular/core';
import { JM } from 'jm-utilities';
import * as _ from 'lodash';

@Pipe({
    name: 'toImdbId'
})
export class ToImdbIdPipe implements PipeTransform {

    transform(value: string, ...args: any[]): any {
        if (JM.isDefined(value)) {
            var stringValue = value.toString();
            return _.padStart(stringValue, 8, '0');
        } else {
            return "";
        }
    }
}
