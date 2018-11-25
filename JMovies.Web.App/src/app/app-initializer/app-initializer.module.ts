import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpService } from '../http.service';
import { TranslateService } from '../translate.service';
import { CommonModule } from '@angular/common';

@NgModule({
    providers: [
        {
            provide: APP_INITIALIZER,
            useFactory: initializeResources,
            multi: true,
            deps: [HttpService, TranslateService]
        }
    ],
    imports: [
        CommonModule
    ],
    declarations: []
})
export class AppInitializerModule { }

export function initializeResources(httpService: HttpService, translateService: TranslateService): () => Promise<any> {

    return (): Promise<any> => {
        return new Promise((resolve, reject) => {
            translateService.initialize().then(() => {
                resolve();
            }, (error) => {
                const errorTitle = translateService.getLocalResource("Title.Error");
                const errorMessage = translateService.getLocalResource("Exception.Common");
                const simpleError = '<div class="custom-error alert alert-danger"><strong>' + errorTitle + '</strong> ' + errorMessage + '</div>';
                $("app-root .loader-overlay").html(simpleError);
            });
        });
    };
}