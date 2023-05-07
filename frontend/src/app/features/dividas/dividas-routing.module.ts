import { RouterModule, Routes } from "@angular/router";
import { DividasListComponent } from "./dividas-list/dividas-list.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
  {
    path: '',
    component: DividasListComponent,
    data: {
      breadcrumb: 'Dividas',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DividasRoutingModule {}
