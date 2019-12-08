import { Injectable, APP_INITIALIZER } from '@angular/core';
import { HttpService } from './http.service';
import { Resource } from './models/general-models/resource';
import { ComplexCollection } from './models/general-models/complexCollection';
import { JM } from 'jm-utilities';

@Injectable({
    providedIn: 'root'
})
export class TranslateService {
    private localResources: any = {
        "en-US": {
            "Title.Error": "Error",
            "Exception.Common": "	We are not able to process your request at the moment, please try again later."
        },
        "tr-TR": {
            "Title.Error": "Hata",
            "Exception.Common": "İsteğinizi şu anda gerçekleştiremiyoruz, lütfen daha sonra tekrar deneyiniz."
        }
    };

    constructor(private httpService: HttpService) {
    }

    public initialize(): Promise<any> {
        const me = this;
        return new Promise((resolve, reject) => {
            me.use();
            me.httpService.get("resources").subscribe((resources: ComplexCollection<Resource>) => {
                if (JM.isDefined(resources)) {
                    me.allResources = resources.$values;
                }

                me.use();
                resolve();
            }, (error) => reject(error));
        });
    }

    public use(cultureCode?: string) {
        const me = this;
        if (!cultureCode) {
            cultureCode = "en-US";
        }

        this.resources = {};
        if (this.allResources && this.allResources.length > 0) {
            this.allResources.filter((item) => {
                return item.culture == cultureCode;
            }).forEach((resource: Resource) => {
                me.resources[resource.key] = resource.value;
            });
        }

        this.activeCulture = cultureCode;
    }

    public translate(key: string): string {
        if (!this.resources) {
            return "";
        }
        if (this.resources[key]) {
            return this.resources[key];
        } else {
            return key;
        }
    }

    public getLocalResource(key: string): string {
        return this.localResources[this.activeCulture][key];
    }

    public resources: any;
    public allResources: Resource[];
    public activeCulture: string;
}
