import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { Production } from '../models/general-models/production';
import { ProductionService } from '../production.service';
import { GetLastAddedProductionsResponse } from '../models/response-models/getLastAddedProductionsResponse';
import { JM } from 'jm-utilities';
import * as _ from 'lodash';

@Component({
    selector: 'app-last-added-productions',
    templateUrl: './last-added-productions.component.html',
    styleUrls: ['./last-added-productions.component.less']
})
export class LastAddedProductionsComponent implements OnInit {

    constructor(private httpService: HttpService, private productionService: ProductionService) { }

    ngOnInit() {
        let self = this;
        this.httpService.postToAction<GetLastAddedProductionsResponse>("GetLastAddedProductions", {}).subscribe((productionsResponse) => {
            self.productions = productionsResponse.productions.$values;
            if (JM.isDefined(self.productions)) {
                _.each(self.productions, (production) => {
                    self.productionService.arrangeProduction(production);
                });
            }
        });
    }

    private productions: Production[];
}
