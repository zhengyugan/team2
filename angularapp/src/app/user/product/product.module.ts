import { NgModule } from '@angular/core';
import { ProductListComponent } from './product-list.component';
import { ProductDetailComponent } from './product-detail.component';
import { RouterModule } from '@angular/router';
import { ProductDetailGuard } from './product-detail.guard';
import { SharedModule } from '../../shared/shared.module';
import { ConvertToSpacesPipe } from '../../shared/convert-to-spaces.pipe';

@NgModule({
  declarations: [
    ProductListComponent,
    ProductDetailComponent,
    ConvertToSpacesPipe
  ],
  imports: [
    SharedModule,
  ]
})
export class ProductModule { }
