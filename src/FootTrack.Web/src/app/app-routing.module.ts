import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainPageComponent } from './home/main-page/main-page.component';
import { AuthGuard } from './core/helpers/auth.guard';

const routes: Routes = [
  {
    path: 'identify',
    loadChildren: () => import('./identify/identify.module').then(m => m.IdentifyModule),
  },
  { path: '', redirectTo: '/home', pathMatch: 'full'},
  { path: 'home', component: MainPageComponent, canActivate: [AuthGuard]},
  { path: '**', component: MainPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
