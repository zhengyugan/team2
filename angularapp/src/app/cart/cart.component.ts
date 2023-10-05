import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CartService } from './cart.service';
import { ICart } from './cart';


@Component({
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit, OnDestroy {
  public pageTitle: string = "My Cart";
  imageWidth: number = 50;
  imageMargin: number = 2;
  sub!: Subscription;

  cart: ICart[] = [];
  errorMessage: string = "";

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.sub = this.cartService.getCart().subscribe({
      next: cart => {
        this.cart = cart;
        console.log(this.cart);
      },
      error: err => this.errorMessage = err
    });
    //this.cart = [{ "productName": "Leaf Rake", "productType": "Red", "price": 19.95, "quantity": 1, "imageUrl": "assets/images/leaf_rake.png" }]
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
