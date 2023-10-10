import { SectionSalesComponent } from './app/report/sections/section-sales/section-sales.component';
import { SectionOrdersComponent } from './app/report/sections/section-orders/section-orders.component';
import { ProductListComponent } from './app/user/product/product-list.component';
import { ProductDetailGuard } from './app/user/product/product-detail.guard';
import { ProductDetailComponent } from './app/user/product/product-detail.component';
import { CartComponent } from './app/cart/cart.component';
import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  { path: 'dashboard', component: SectionSalesComponent },
  { path: 'orders', component: SectionOrdersComponent },
  { path: 'products', component: ProductListComponent },
  { path: 'cart', component: CartComponent },
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', redirectTo: 'products', pathMatch: 'full' },
  {
    path: 'products/:id',
    canActivate: [ProductDetailGuard],
    component: ProductDetailComponent
  }
]
