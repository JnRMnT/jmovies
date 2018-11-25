import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

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

@NgModule({
    declarations: [
        AppComponent,
        TranslatePipe,
        AppHeaderComponent,
        AppFooterComponent
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
        AppInitializerModule
    ],
    providers: [HttpService, LoadingService, TranslateService],
    bootstrap: [AppComponent]
})
export class AppModule { }
