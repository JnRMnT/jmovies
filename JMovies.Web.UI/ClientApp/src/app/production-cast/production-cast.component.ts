import { Component, OnInit } from '@angular/core';
import { ProductionService } from '../production.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Movie } from '../models/general-models/movie';
import { TVSeries } from '../models/general-models/tv-series';
import { BaseProduction } from '../models/general-models/production';

@Component({
  selector: 'app-production-cast',
  templateUrl: './production-cast.component.html',
  styleUrls: ['./production-cast.component.less']
})
export class ProductionCastComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productionService: ProductionService) { }

  ngOnInit() {
    const vm = this;
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        vm.productionService.getProductionDetails(params.get('id')))
    ).subscribe((production: BaseProduction) => {
      vm.production = <Movie | TVSeries>production;
    }, (error) => {
      console.error(error);
    });
  }

  public production: Movie | TVSeries;
  public currentPage = "production-cast";
}
