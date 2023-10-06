import { NgModule } from '@angular/core';
import { ConvertToSpacesPipe } from '../../shared/convert-to-spaces.pipe';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProductListComponent } from './product-list.component';
import { ProductDetailsComponent } from './product-details.component';
import { ProductDetailGuard } from './product-detail.guard';
import { AppModule } from 'src/app/app.module';
import { PaginationComponent } from 'src/app/shared/pagination/pagination.component';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';



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
    ReactiveFormsModule,
    FontAwesomeModule,
    CommonModule
  ],
  providers: [
    FontAwesomeModule
  ]
})
export class ProductModule {
}
