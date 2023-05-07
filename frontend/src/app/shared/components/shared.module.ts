import { NgModule } from "@angular/core";
import { FormInputComponent } from "./form-input/form-input.component";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

const COMPONENTS = [
  FormInputComponent,
];

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule,
    FormsModule
  ],
  declarations: [...COMPONENTS],
  exports: [
    ...COMPONENTS,
  ],
  providers: [],
})

export class SharedModule {}
