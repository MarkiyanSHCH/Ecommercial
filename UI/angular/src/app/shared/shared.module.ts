import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { SpinnerComponent } from './spinner/spinner.component';
import { LoadableComponent } from './loadable/loadable.component';

import { BothMatchDirective } from './validation';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    RouterModule,

    LoadableComponent,
    SpinnerComponent,

    BothMatchDirective
  ],
  declarations: [
    LoadableComponent,
    SpinnerComponent,

    BothMatchDirective
  ]
})
export class SharedModule { }
