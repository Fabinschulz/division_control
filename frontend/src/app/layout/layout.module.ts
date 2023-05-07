import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { HeaderMenuComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [
    LayoutComponent,
    HeaderMenuComponent,
    FooterComponent
  ],
  exports: [
    LayoutComponent,
    HeaderMenuComponent,
    FooterComponent
  ],
})
export class LayoutModule {}
