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
import { MovieComponent } from './movie/movie.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { JmLinkComponent } from './jm-link/jm-link.component';
import { JmButtonComponent } from './jm-button/jm-button.component';
import { MovieNavigationComponent } from './movie-navigation/movie-navigation.component';
import { MovieCastComponent } from './movie-cast/movie-cast.component';
import { MovieService } from './movie.service';
import { MovieCreditsComponent } from './movie-credits/movie-credits.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    TranslatePipe,
    AppHeaderComponent,
    AppFooterComponent,
    MovieComponent,
    PageNotFoundComponent,
    DashboardComponent,
    JmLinkComponent,
    JmButtonComponent,
    MovieNavigationComponent,
    MovieCastComponent,
    MovieCreditsComponent,
    NavMenuComponent
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
  providers: [HttpService, LoadingService, TranslateService, MovieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
