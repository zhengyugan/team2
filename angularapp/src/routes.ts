import { Routes } from '@angular/router';

import { SectionSalesComponent } from './app/report/sections/section-sales/section-sales.component';
import { SectionOrdersComponent } from './app/report/sections/section-orders/section-orders.component';

import { ProductListComponent } from './app/user/product/product-list.component';
import { ProductDetailsComponent } from './app/user/product/product-details.component';
import { ProductDetailGuard } from './app/user/product/product-detail.guard';
import { LoginComponent } from './app/login/login.component';
import { SignupComponent } from './app/signup/signup.component';

export const appRoutes: Routes = [
  {path:'login', component: LoginComponent},
  {path: 'signup', component: SignupComponent},
  { path: 'dashboard', component: SectionSalesComponent },
  { path: 'orders', component: SectionOrdersComponent },
  {
    path: 'products/:id',
    canActivate: [ProductDetailsComponent],
    component: ProductDetailsComponent
  },
  { path: 'products', component: ProductListComponent },
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', redirectTo: 'products', pathMatch: 'full' },
  
]
