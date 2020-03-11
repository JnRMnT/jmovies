import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProductionComponent } from './production/production.component';
import { ProductionCastComponent } from './production-cast/production-cast.component';
import { LoginComponent } from './login/login.component';
import { AuthenticationGuard } from './authentication.guard';
import { RegisterComponent } from './register/register.component';
import { RegisterCompleteComponent } from './register-complete/register-complete.component';

const routes: Routes = [
    { path: 'app/movie/:id', component: ProductionComponent },
    { path: 'app/tvseries/:id', component: ProductionComponent },
    { path: 'app/production/:id', component: ProductionComponent },
    { path: 'app/production-cast/:id', component: ProductionCastComponent },
    { path: 'app/movie-cast/:id', component: ProductionCastComponent },
    { path: 'app/tv-series-cast/:id', component: ProductionCastComponent },
    { path: 'app/dashboard', component: DashboardComponent },
    { path: '', redirectTo: 'app/dashboard', pathMatch: 'full' },
    { path: 'not-found', component: PageNotFoundComponent },
    { path: 'error', component: PageNotFoundComponent },
    { path: 'logout', component: PageNotFoundComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'register-complete', component: RegisterCompleteComponent },
    { path: '**', component: PageNotFoundComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
