import { NgModule } from '@angular/core';
import { CartComponent } from './cart.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CommonModule } from '@angular/common';
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
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    CommonModule
  ]
})
export class CartModule { }
