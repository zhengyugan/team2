import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminProductListComponent } from './product-list/product-list.component';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AdminProductDetailComponent } from './product-detail/product-detail.component';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { AdminProductCreateComponent } from './product-create/product-create.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [
    AdminProductListComponent,
    AdminProductDetailComponent,
    FileUploadComponent,
    AdminProductCreateComponent,
  ],
  imports: [
    FontAwesomeModule,
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {
        path: 'admin/product/create',
        // canActivate: [ProductDetailGuard],
        component: AdminProductCreateComponent
      },
      {
        path: 'admin/product/:id',
        // canActivate: [ProductDetailGuard],
        component: AdminProductDetailComponent
      },
    ]),
  ]
})
export class AdminProductModule { }
