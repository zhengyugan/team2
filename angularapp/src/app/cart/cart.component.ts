import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CartService } from './cart.service';
import { ICart } from './cart';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

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
  total: string = "";
  subtotal: string = "";
  errorMessage: string = "";

  constructor(private cartService: CartService, private modalService: NgbModal) { }

  open() {
    const modalRef = this.modalService.open(NgbdModalContent);
    modalRef.componentInstance.customMessage = 'Payment Completed!';
  }

  ngOnInit(): void {
    this.sub = this.cartService.getCart().subscribe({
      next: cart => {
        this.cart = cart;
        console.log(this.cart);

        this.total = "1240";
        this.subtotal = "1240";
      },
      error: err => this.errorMessage = err
    });
    //this.cart = [{ "productName": "Leaf Rake", "productType": "Red", "price": 19.95, "quantity": 1, "imageUrl": "assets/images/leaf_rake.png" }]
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
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
    // Redirect to the product page
    this.router.navigate(['../products']);
    this.activeModal.close();
  }
}

export class NgbdModalComponent {
  constructor(private modalService: NgbModal) { }

  open() {
    const modalRef = this.modalService.open(NgbdModalContent);
  }
}
