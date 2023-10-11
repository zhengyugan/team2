import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StarComponent } from './star/star.component';
import { FormsModule } from '@angular/forms';
import { PaginationComponent } from './pagination/pagination.component';
import { ImageComponent } from './image/image.component';



@NgModule({
  declarations: [
    StarComponent,
    PaginationComponent,
    ImageComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    StarComponent,
    PaginationComponent
  ]
})
export class SharedModule { }
