import { ApplicationContext } from './models/general-models/application-context';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatRippleModule } from '@angular/material/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TranslatePipe } from './translate.pipe';
import { NgxLoadingModule } from 'ngx-loading';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AppInitializerModule } from './app-initializer/app-initializer.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpService } from './http.service';
import { TranslateService } from './translate.service';
import { LoadingService } from './loading.service';
import { AppHeaderComponent } from './app-header/app-header.component';
import { AppFooterComponent } from './app-footer/app-footer.component';
import { ProductionComponent } from './production/production.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { JmLinkComponent } from './jm-link/jm-link.component';
import { JmButtonComponent } from './jm-button/jm-button.component';
import { ProductionNavigationComponent } from './production-navigation/production-navigation.component';
import { ProductionCastComponent } from './production-cast/production-cast.component';
import { ProductionService } from './production.service';
import { ProductionCreditsComponent } from './production-credits/production-credits.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ToMoviePipe } from './to-movie.pipe';
import { ToTVSeriesPipe } from './to-tvseries.pipe';
import { ToBase64Pipe } from './to-base64.pipe';
import { JmResultPanelComponent } from './jm-result-panel/jm-result-panel.component';
import { ResultHandlingService } from './result-handling.service';
import { LastAddedProductionsComponent } from './last-added-productions/last-added-productions.component';
import { ProductionPreviewComponent } from './production-preview/production-preview.component';
import { ProductionTitlePipe } from './production-title.pipe';
import { AppSearchComponent } from './app-search/app-search.component';
import { ToImdbIdPipe } from './to-imdbid.pipe';
import { ToStringPipe } from './to-string.pipe';
import { LoginComponent } from './login/login.component';
import { AuthenticationService } from './authentication.service';
import { JwtModule } from '@auth0/angular-jwt';
import { JM } from 'jm-utilities';
import { RegisterComponent } from './register/register.component';
import { RegisterCompleteComponent } from './register-complete/register-complete.component';

@NgModule({
    declarations: [
        AppComponent,
        TranslatePipe,
        AppHeaderComponent,
        AppFooterComponent,
        ProductionComponent,
        PageNotFoundComponent,
        DashboardComponent, 
        JmLinkComponent,
        JmButtonComponent,
        ProductionNavigationComponent,
        ProductionCastComponent,
        ProductionCreditsComponent,
        NavMenuComponent,
        ToMoviePipe,
        ToTVSeriesPipe,
        ToBase64Pipe,
        JmResultPanelComponent,
        LastAddedProductionsComponent,
        ProductionPreviewComponent,
        ProductionTitlePipe,
        AppSearchComponent,
        ToImdbIdPipe,
        ToStringPipe,
        LoginComponent,
        RegisterComponent,
        RegisterCompleteComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        CommonModule,
        NgxLoadingModule.forRoot({
            fullScreenBackdrop: true
        }),
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        AppInitializerModule,
        MatButtonToggleModule,
        MatButtonModule,
        MatRippleModule,
        MatGridListModule,
        MatCardModule,
        MatAutocompleteModule,
        MatFormFieldModule,
        JwtModule.forRoot({
            config: {
                tokenGetter: getToken
            }
        }),
        ToastrModule.forRoot()
    ],
    providers: [HttpService, LoadingService, TranslateService, ProductionService, ResultHandlingService, AuthenticationService ],
    bootstrap: [AppComponent]
})
export class AppModule { }

export function getToken(): string {
    return window.localStorage.getItem("jmAuthToken");
}
