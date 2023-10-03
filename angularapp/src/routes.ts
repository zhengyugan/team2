import { Routes } from '@angular/router';

import { SectionSalesComponent } from './app/report/sections/section-sales/section-sales.component';
import { SectionOrdersComponent } from './app/report/sections/section-orders/section-orders.component';

import { ProductListComponent } from './app/user/products/product-list.component';
import { ProductDetailGuard } from './app/user/products/product-detail.guard';
import { ProductDetailComponent } from './app/user/products/product-detail.component';

export const appRoutes: Routes = [
  { path: 'dashboard', component: SectionSalesComponent },
  { path: 'orders', component: SectionOrdersComponent },
  { path: 'products', component: ProductListComponent },
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', redirectTo: 'products', pathMatch: 'full' },
  {
    path: 'products/:id',
    canActivate: [ProductDetailGuard],
    component: ProductDetailComponent
  }
]
