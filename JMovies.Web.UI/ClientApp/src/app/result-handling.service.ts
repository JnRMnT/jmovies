import { Injectable, Injector } from '@angular/core';
import { JMResult } from './models/general-models/result-handling/jm-result';
import { JM } from 'jm-utilities';
import { RedirectionTypeEnum } from './models/general-models/result-handling/redirection-type-enum';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})
export class ResultHandlingService {

    constructor(private injector: Injector) { }

    handleResult(result: JMResult) {
        if (JM.isDefined(result)) {
            this.handleRedirection(result);
            this.handleMessageDisplay(result);
        }
    }

    private handleRedirection(result: JMResult) {
        if (JM.isDefined(result.redirectionInfo)) {
            if (result.redirectionInfo.redirectionType == RedirectionTypeEnum.RedirectToErrorPage) {
                result.redirectionInfo.redirectionParameter = "error";
            }
            else if (result.redirectionInfo.redirectionType == RedirectionTypeEnum.RedirectToLogout) {
                result.redirectionInfo.redirectionParameter = "logout";
            }
            switch (result.redirectionInfo.redirectionType) {
                case RedirectionTypeEnum.RedirectToErrorPage:
                case RedirectionTypeEnum.RedirectToRoute:
                    this.getRouter().navigate([result.redirectionInfo.redirectionParameter]);
                    break;
                default:
                    break;
            }
        }
    }

    private handleMessageDisplay(result: JMResult) {
        if (JM.isDefined(result.errors)) {
            result.errors.$values.forEach((value, index) => {
                this.getToastr().error(value.message);
            });
        }
        if (JM.isDefined(result.warnings)) {
            result.warnings.$values.forEach((value, index) => {
                this.getToastr().warning(value.message);
            });
        }
        if (JM.isDefined(result.informations)) {
            result.informations.$values.forEach((value, index) => {
                this.getToastr().info(value.message);
            });
        }
    }

    private getRouter(): Router {
        if (!JM.isDefined(this.router)) {
            this.router = this.injector.get(Router);
        }
        return this.router;
    }
    private getToastr(): ToastrService {
        if (!JM.isDefined(this.toastrService)) {
            this.toastrService = this.injector.get(ToastrService);
        }
        return this.toastrService;
    }

    private router: Router;
    private toastrService: ToastrService
}
