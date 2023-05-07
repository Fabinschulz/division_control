import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { DividasComponent } from './features/dividas/dividas.component';
import { DividasListComponent } from './features/dividas/dividas-list/dividas-list.component';
import { DividasCreateComponent } from './features/dividas/dividas-create/dividas-create.component';
import { DividasEditComponent } from './features/dividas/dividas-edit/dividas-edit.component';
import { DividasViewComponent } from './features/dividas/dividas-view/dividas-view.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    loadChildren: () => import('./features/feature.module').then(m => m.FeatureModule),
  },
  { path: 'features/dividas', component: DividasListComponent },
  { path: 'features/dividas/dividas-create', component: DividasCreateComponent },
  { path: 'features/dividas/dividas-edit/:id', component: DividasEditComponent },
  { path: 'features/dividas/dividas-view/:id', component: DividasViewComponent },
  { path: 'home', component: LayoutComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
