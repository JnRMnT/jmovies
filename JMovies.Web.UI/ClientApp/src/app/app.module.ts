import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatButtonModule, MatButtonToggleModule } from '@angular/material';
import 'hammerjs';

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
    ToTVSeriesPipe
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
    MatButtonModule
  ],
  providers: [HttpService, LoadingService, TranslateService, ProductionService],
  bootstrap: [AppComponent]
})
export class AppModule { }
