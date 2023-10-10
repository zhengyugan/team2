import { NgModule } from '@angular/core';
import { CartComponent } from './cart.component';
import { SharedModule } from '../shared/shared.module';
//import { RouterModule } from '@angular/router';
//import { NgbdModalContent } from '../app.component';



@NgModule({
  declarations: [
    CartComponent
    //NgbdModalContent
  ],
  imports: [
    //RouterModule.forRoot([
    //  { path: 'cart', component: CartComponent }
    //]),
    SharedModule
  ]
})
export class CartModule { }
