import { Component, OnInit } from '@angular/core';

import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { HttpService } from '../http.service';
import { GetProductionCastRequest } from '../models/request-models/getProductionCastRequest';
import { GetProductionCastResponse } from '../models/response-models/getProductionCastResponse';
import { Credit } from '../models/general-models/credit';
import { JM } from 'jm-utilities';

@Component({
    selector: 'app-production-cast',
    templateUrl: './production-cast.component.html',
    styleUrls: ['./production-cast.component.less']
})
export class ProductionCastComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private httpService: HttpService) { }

    ngOnInit() {
        const vm = this;
        this.route.paramMap.subscribe((params: ParamMap) => {
            vm.productionID = params.get('id');
            let request: GetProductionCastRequest = {
                productionID: vm.productionID
            };
            vm.httpService.postToAction<GetProductionCastResponse>("GetProductionCast", request).subscribe((getProductionCastResponse) => {
                if (JM.isDefined(getProductionCastResponse) && JM.isDefined(getProductionCastResponse.cast)) {
                    vm.cast = getProductionCastResponse.cast.$values;
                }
            });
        });
    }

    public cast: Credit[];
    public productionID: string | number;
    public currentPage = "production-cast";
}
