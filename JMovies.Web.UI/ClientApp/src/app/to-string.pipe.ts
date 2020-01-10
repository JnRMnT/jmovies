import { Pipe, PipeTransform } from '@angular/core';
import { JM } from 'jm-utilities';
import * as _ from 'lodash';
import { TranslateService } from './translate.service';

@Pipe({
    name: 'toString'
})
export class ToStringPipe implements PipeTransform {
    constructor(private translateService: TranslateService) {

    }

    transform(value: any, ...args: any[]): any {
        const self = this;
        if (JM.isDefined(value)) {
            const seperator: string = JM.isDefined(args) && args.length > 0 && JM.isDefined(args[0]) ? args[0] : ', ';
            const translatePrefix: string = JM.isDefined(args) && args.length > 1 && JM.isDefined(args[1]) ? args[1] : undefined;
            const complexArrayProperty: string = JM.isDefined(args) && args.length > 2 && JM.isDefined(args[2]) ? args[2] : undefined;
            const limit: number = JM.isDefined(args) && args.length > 3 && JM.isDefined(args[3]) ? args[3] : undefined;

            let sourceArray: any[] = value;
            let valueArray = [];

            if (!_.isArray(value) && JM.isDefined(value.$type) && JM.isDefined(value.$values)) {
                sourceArray = value.$values;
            }

            _.each(sourceArray, function (value) {
                let valueToAdd = value;
                if (JM.isDefined(complexArrayProperty)) {
                    valueToAdd = value[complexArrayProperty];
                }
                if (JM.isDefined(translatePrefix)) {
                    valueToAdd = self.translateService.translate(translatePrefix + "." + valueToAdd);
                }
                valueArray.push(valueToAdd);
            });

            if (JM.isDefined(limit)) {
                valueArray = _.take(valueArray, limit);
            }

            return _.join(valueArray, seperator);
        } else {
            return '';
        }
    }
}
