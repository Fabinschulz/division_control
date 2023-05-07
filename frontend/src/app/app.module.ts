import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { LayoutModule } from './layout/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './shared/components/shared.module';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';
import { NgxMaskModule } from 'ngx-mask-2';
import { TextMaskModule } from 'angular2-text-mask';
import { httpInterceptorsProviders } from './core';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    ToastrModule.forRoot({}),
    BrowserAnimationsModule,
    NgxMaskModule,
    TextMaskModule,
    HttpClientModule
  ],
  providers: [
    httpInterceptorsProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
