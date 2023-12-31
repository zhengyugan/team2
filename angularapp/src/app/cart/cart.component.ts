import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { CartService } from './cart.service';
import { ICart } from './cart';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { OrderUpdateModel } from '../shared/order-update';
import { Carts } from '../shared/cart';

@Component({
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit, OnDestroy {
  public pageTitle: string = "My Cart";
  imageWidth: number = 50;
  imageMargin: number = 2;
  sub!: Subscription;
  sub1!: Subscription;
  sub2!: Subscription;
  sub3!: Subscription;
  userId: number = 3;
  cart: ICart[] = [];
  total: number = 0;
  subtotal: string = "";
  errorMessage: string = "";
  isButtonDisabled = false;

  form = new FormGroup({
    quantity: new FormControl('', Validators.required)
  });  

  constructor(private cartService: CartService, private modalService: NgbModal) { }

  open() {
    var orderUpdate:OrderUpdateModel = {
      "total": this.total,
      "modifiedby": this.userId
    }

    this.createOrder(this.userId, orderUpdate);
    const modalRef = this.modalService.open(NgbdModalContent);
    modalRef.componentInstance.customMessage = 'Payment Completed!';
  }

  ngOnInit(): void {
    this.getCartItem();
    this.getFormControlsFields(this.cart);
    //this.calculateTotal();
  }

  getCartItem() {
    this.sub = this.cartService.getCartItem(this.userId).subscribe({
      next: cart => {
        this.cart = cart['Data'];
        console.log(this.cart);
        this.calculateTotal();
      },
    });
  }

  updateQuantity(operation: string): void {
    var initialQuantity: number = Number(this.form.controls['quantity'].value);

    if (operation == "minus" && initialQuantity > 0) {
      initialQuantity--;
      this.form.controls['quantity'].setValue(initialQuantity.toString());
      if (initialQuantity == 0) {
        this.isButtonDisabled = true;
      }
    } else {
      initialQuantity++;
      this.isButtonDisabled = false;
      this.form.controls['quantity'].setValue(initialQuantity.toString());
    }
  }

  onDelete(id: number) {
    this.sub1 = this.cartService.delete(id).subscribe({
      next: cart => {
        this.cart = cart['Data'];
      },
    });
    location.reload();
  }

  createOrder(userid: number, orderUpdate: OrderUpdateModel) {
    console.log(userid, orderUpdate);
    this.sub2 = this.cartService.createOrder(userid, orderUpdate).subscribe({
      next: cart => {
        console.log(789);
        this.cart = cart['Data'];
      },
    });
  }

  calculateTotal() {
    console.log(123);
    console.log(this.cart);

    this.total = this.cart.reduce((acc, product) => {
      // Check if product is not deleted and not modified
      if (!product.deleted_at && !product.moodified_at) {
        return acc + (product.price * product.quantity);
      } else {
        return acc; // Exclude deleted or modified products from the total
      }
    }, 0);
  }

  getFormControlsFields(cart: ICart []) {
    const formGroupFields = {};
    cart.forEach(item => {

      console.log(item)

    })  
    return formGroupFields;
  }

  ngOnDestroy(): void {
    this.sub3.unsubscribe();
  }
}

@Component({
  selector: 'ngbd-modal-content',
  templateUrl: '../modal/success-modal/modal.component.html',
})
export class NgbdModalContent {
  @Input() customMessage: any;
  constructor(public activeModal: NgbActiveModal, private router: Router) { }

  onClose() {
    this.router.navigate(['./products']);
    this.activeModal.close();
  }
}

export class NgbdModalComponent {
  constructor(private modalService: NgbModal) { }

  open() {
    const modalRef = this.modalService.open(NgbdModalContent);
  }
}
