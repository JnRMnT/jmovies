import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MovieComponent } from './movie/movie.component';
import { MovieCastComponent } from './movie-cast/movie-cast.component';

const routes: Routes = [
    { path: 'app/movie/:id', component: MovieComponent },
    { path: 'app/movie-cast/:id', component: MovieCastComponent },
    { path: 'app/dashboard', component: DashboardComponent },
    { path: '', redirectTo: 'app/dashboard', pathMatch: 'full' },
    { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }