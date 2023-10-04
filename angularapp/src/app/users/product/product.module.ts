import { NgModule } from '@angular/core';
import { ConvertToSpacesPipe } from '../../shared/convert-to-spaces.pipe';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductListComponent } from './product-list.component';
import { ProductDetailsComponent } from './product-details.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from 'src/routes';
import { ProductDetailGuard } from './product-detail.guard';

@NgModule({
  declarations: [
    ProductListComponent,
    ConvertToSpacesPipe,
    ProductDetailsComponent
  ],
  imports: [
    RouterModule.forRoot([
      {
        path: 'products/:id',
        canActivate: [ProductDetailGuard],
        component: ProductDetailsComponent
      }
    ]),
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class ProductModule {
}
