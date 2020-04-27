import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainPageComponent } from './home/main-page/main-page.component';
import { AuthGuard } from './core/helpers/auth.guard';

const routes: Routes = [
  {
    path: 'identify',
    loadChildren: () => import('./identify/identify.module').then(m => m.IdentifyModule),
  },
  { path: '', component: MainPageComponent, canActivate: [AuthGuard] },

  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
