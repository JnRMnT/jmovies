import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition, AnimationEvent } from '@angular/animations';
import { Observable, Observer } from 'rxjs';
import { FormControl } from '@angular/forms';
import { startWith, map, distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { HttpService } from '../http.service';
import { JM } from 'jm-utilities';
import * as _ from 'lodash';
import { SearchRequest } from '../models/request-models/searchRequest';
import { SearchResponse } from '../models/response-models/searchResponse';
import { Production } from '../models/general-models/production';
import { MatOption, MatOptionSelectionChange } from '@angular/material/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-search',
    templateUrl: './app-search.component.html',
    styleUrls: ['./app-search.component.less'],
    host: {
        '[@openClose]': 'isOpen',
        '(@openClose.done)': 'onAnimationCompleted($event)',
    },
    animations: [
        trigger('openClose', [
            state('open', style({
                width: '16em',
                opacity: 1
            })),
            state('closed', style({
                opacity: 0,
                width: 0,
                margin: 0,
                padding: 0
            })),
            transition('open => closed', [
                animate('0.5s')
            ]),
            transition('closed => open', [
                animate('0.5s')
            ])
        ]),
    ]
})
export class AppSearchComponent implements OnInit {
    searchControl = new FormControl();
    isOpen = false;
    isHidden = true;
    searchKey: string;
    filteredOptions: Observable<any[]>;
    constructor(private httpService: HttpService, private router: Router) {
    }

    public selected(optionChangeEvent: MatOptionSelectionChange) {
        var production = optionChangeEvent.source.value;
        this.router.navigate(["app/production", production.id]);
        this.searchControl.setValue("");
    }
    onAnimationCompleted(event: AnimationEvent) {
        if (<any>(event.toState) == false && event.phaseName == "done") {
            this.isHidden = true;
        }
    }

    ngOnInit() {
        var vm = this;

        this.searchControl.valueChanges
            .pipe(debounceTime(400))
            .pipe(distinctUntilChanged())
            .subscribe(function (value) {
                if (!JM.isDefined(value) || !_.isString(value) || value.length < 3) {
                    vm.filteredOptionsObserver.next([]);
                }
                else {
                    var searchRequest: SearchRequest = {
                        searchKey: value
                    };
                    vm.httpService.postToAction<SearchResponse>("Search", searchRequest).subscribe(function (response) {
                        var searchResults = [];
                        if (JM.isDefined(response) && JM.isDefined(response.searchResults) && JM.isDefined(response.searchResults.$values)) {
                            searchResults = response.searchResults.$values.map(e => e.source);
                        }
                        vm.filteredOptionsObserver.next(searchResults);
                    });
                }
            });

        this.filteredOptions = Observable.create((observer: Observer<any[]>) => {
            vm.filteredOptionsObserver = observer;
        });
    }

    public toggleSearchBox(): void {
        this.isOpen = !this.isOpen;
        if (this.isOpen) {
            this.isHidden = false;
        }
    }

    private filteredOptionsObserver: Observer<any[]>;
}
