import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { GetProductionDetailsResponse } from '../models/response-models/getProductionDetailsResponse';
import { Movie } from '../models/general-models/movie';
import * as _ from 'lodash';
import { ProductionService } from '../production.service';
import { ActingCredit } from '../models/general-models/acting-credit';
import { JM } from 'jm-utilities';
import { CreditRoleType } from '../models/general-models/credit-role-type-enum';
import { Production } from '../models/general-models/production';
import { TVSeries } from '../models/general-models/tv-series';
import { ProductionTypeEnum } from '../models/general-models/production-type-enum';

@Component({
  selector: 'app-production',
  templateUrl: './production.component.html',
  styleUrls: ['./production.component.less']
})
export class ProductionComponent implements OnInit, OnDestroy {

  constructor(
    public route: ActivatedRoute,
    public router: Router,
    public productionService: ProductionService) {
    this.boundResizeFunction = this.onResize.bind(this);
  }

  ngOnInit() {
    const vm = this;
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        vm.productionService.getProductionDetails(params.get('id')))
    ).subscribe((production: Production) => {
      vm.production = production;
      if (JM.isDefined(vm.production)) {
        let summaryCount = 0;
        if (vm.production.productonType == ProductionTypeEnum.Movie) {
          var movie = <Movie>vm.production;
          vm.summaryActingCredits = _.filter(<ActingCredit[]>movie.credits, (credit: ActingCredit): boolean => {
            if (credit.roleType == CreditRoleType.Acting && summaryCount < 5) {
              summaryCount++;
              return true;
            } else {
              return false;
            }
          });
        }
      }
      $(window).on("resize", vm.boundResizeFunction);
      _.defer(function () {
        vm.checkScrollElement();
      });
    }, (error) => {
      console.error(error);
    });
  }

  ngOnDestroy(): void {
    $(window).off("resize", this.boundResizeFunction);
    $(".main-actors>.slimScrollDiv").slimScroll({ destroy: true });
  }

  private onResize() {
    this.checkScrollElement();
  }
  private boundResizeFunction: any;

  private checkScrollElement(): void {
    var self = this;
    if ($(window).width() < 768) {
      $(".main-actors>.slimScrollDiv").slimScroll({ destroy: true });
    } else if ($(".main-actors>.flex-column").length == 0) {
      $(".main-actors>.flex-column").slimScroll();
    }
  }

  public production: Production | Movie | TVSeries;
  public summaryActingCredits: ActingCredit[];
  public currentPage = "production";
}
