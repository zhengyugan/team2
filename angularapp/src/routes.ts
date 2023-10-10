import { Routes } from '@angular/router';
import { SectionSalesComponent } from './app/admin/report/sections/section-sales/section-sales.component';
import { SectionOrdersComponent } from './app/admin/report/sections/section-orders/section-orders.component';
import { ProductListComponent } from './app/user/product/product-list.component';
import { ProductDetailsComponent } from './app/user/product/product-details.component';
import { ProductDetailGuard } from './app/user/product/product-detail.guard';
import { CartComponent } from './app/cart/cart.component';
import { LoginComponent } from './app/login/login.component';
import { SignupComponent } from './app/signup/signup.component';

export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'dashboard', component: SectionSalesComponent },
  { path: 'orders', component: SectionOrdersComponent },
  { path: 'products', component: ProductListComponent },
  { path: 'cart', component: CartComponent },
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', redirectTo: 'products', pathMatch: 'full' },
  {
    path: 'products/:id',
    canActivate: [ProductDetailGuard],
    component: ProductDetailsComponent
  },
  { path: 'products', component: ProductListComponent },
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', redirectTo: 'products', pathMatch: 'full' }
]
