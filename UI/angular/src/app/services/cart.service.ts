import { Inject, Injectable } from '@angular/core';

import { CartItem } from '../models/cart/cartItem';

export const CART_ITEMS = ('CartItemArray');

@Injectable({
  providedIn: 'root'
})

export class CartService {

  private _cartItems: CartItem[] = <CartItem[]>[]

  constructor() { }

  addItemCart(id: number, quantity: number, note: string) {
    this._cartItems = JSON.parse(localStorage.getItem(CART_ITEMS) || '[]');
    quantity += this._cartItems.find(x => x.productId === id)?.quantity ?? 0;
    this._cartItems = this._cartItems.filter(x => x.productId !== id);
    this._cartItems.push(<CartItem>{
      productId: id,
      quantity: quantity,
      note: note
    });

    localStorage.setItem(CART_ITEMS, JSON.stringify(this._cartItems));
  }
}
