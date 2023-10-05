import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CartComponent } from './cart.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    CartComponent
  ],
  imports: [
    RouterModule.forRoot([
      { path: 'cart', component: CartComponent }
    ]),
    SharedModule
  ]
})
export class CartModule { }
