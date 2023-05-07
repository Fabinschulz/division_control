import { NgModule } from "@angular/core";
import { DividasListComponent } from "./dividas-list/dividas-list.component";
import { CommonModule } from "@angular/common";
import { DividasRoutingModule } from "./dividas-routing.module";
import { DividasComponent } from "./dividas.component";
import { SharedModule } from "src/app/shared/components/shared.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { DividasCreateComponent } from "./dividas-create/dividas-create.component";
import { HttpClientModule } from '@angular/common/http';
import { DividasService } from "./services/dividas.service";
import { TextMaskModule } from "angular2-text-mask";
import { DividasEditComponent } from "./dividas-edit/dividas-edit.component";
import { ToastrModule } from "ngx-toastr";
import { DividasViewComponent } from "./dividas-view/dividas-view.component";

@NgModule({
  declarations: [
    DividasComponent,
    DividasListComponent,
    DividasCreateComponent,
    DividasEditComponent,
    DividasViewComponent
  ],
  imports:
  [
    CommonModule,
    DividasRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [
    DividasService,
  ]
})
export class DividasModule {}
