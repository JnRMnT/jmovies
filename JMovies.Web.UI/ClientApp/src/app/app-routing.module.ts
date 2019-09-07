import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProductionComponent } from './production/production.component';
import { ProductionCastComponent } from './production-cast/production-cast.component';

const routes: Routes = [
  { path: 'app/movie/:id', component: ProductionComponent },
  { path: 'app/tvseries/:id', component: ProductionComponent },
  { path: 'app/production/:id', component: ProductionComponent },
  { path: 'app/movie-cast/:id', component: ProductionCastComponent },
  { path: 'app/tv-series-cast/:id', component: ProductionCastComponent },
  { path: 'app/dashboard', component: DashboardComponent },
  { path: '', redirectTo: 'app/dashboard', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
