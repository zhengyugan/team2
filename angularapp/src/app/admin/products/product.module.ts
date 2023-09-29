import { NgModule } from '@angular/core';
import { ProductListComponent } from '././product-list/product-list.component';
import { ProductDetailComponent } from '././product-detail/product-detail.component';
import { ConvertToSpacesPipe } from '../../shared/convert-to-spaces.pipe';
import { RouterModule } from '@angular/router';
import { ProductDetailGuard } from '././product-detail/product-detail.guard';
import { SharedModule } from '../../shared/shared.module';
import { ProductCreateComponent } from './product-create/product-create.component';



@NgModule({
  declarations: [
    ProductListComponent,
    ProductDetailComponent,
    ConvertToSpacesPipe,
    ProductCreateComponent
  ],
  imports: [
    RouterModule.forRoot([
      { 
        path: 'admin/products', 
        component: ProductListComponent 
      },
      {
        path: 'admin/products/create',
        component: ProductCreateComponent
      },
      {
        path: 'admin/products/:id',
        canActivate: [ProductDetailGuard],
        component: ProductDetailComponent
      }
    ]),
    SharedModule
  ]
})
export class ProductModule { }
