import { Component, EventEmitter, Input, Output } from '@angular/core';

import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { CartItem } from 'src/app/models/cart/cartItem';
import { Product } from 'src/app/models/product/product';

@Component({
  selector: 'app-cart-dashboard',
  templateUrl: './cart-dashboard.component.html',
  styleUrls: ['./cart-dashboard.component.css'],
})
export class CartModuleComponent {

  model: CartItem = <CartItem>{
    quantity: 1
  };

  productModal: Product = <Product>{};

  @Input() set product(item: Product) {
    this.productModal = item;
    this.model.productId = item.id;
  }

  @Output() cartItem = new EventEmitter<CartItem>();

  constructor(public activeModal: NgbActiveModal) { }

  incrementQuantity() {
    this.model.quantity++;
  }

  decrementQuantity() {
    if (this.model.quantity > 1)
      this.model.quantity--;
  }
}
