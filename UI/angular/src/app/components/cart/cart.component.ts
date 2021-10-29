import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { CartItem } from 'src/app/models/cart/cartItem';
import { Product } from 'src/app/models/product/product';
import { OrderDashboardComponent } from 'src/app/module/order-dashboard/order-dashboard.component'
import { Order } from 'src/app/models/order/order';
import { OrderHttpService } from 'src/app/services/http/order.http.service';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { Shop } from 'src/app/models/shop/shop';
import { ShopHttpService } from 'src/app/services/http/shop.http.service';
import { CartHttpService } from 'src/app/services/http/cart.http.service';

export const CART_ITEMS = ('CartItemArray');

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  private _productIds: number[] = <number[]>[]

  totalPrice: number = 0;
  cartItems: CartItem[] = <CartItem[]>[]
  items: Product[] = <Product[]>[];
  shops: Shop[] = <Shop[]>[];

  constructor(
    private _modalService: NgbModal,
    private _cartHttpService: CartHttpService,
    private _orderHttpService: OrderHttpService,
    private _authService: AuthService,
    private _shopHttpService: ShopHttpService,
    private _router: Router
  ) { }

  ngOnInit() {
    this.loadPageInfo();
  }

  returnProduct(productId: number): Product {
    return this.items.find(x => x.id === productId) ?? <Product>{};
  }

  loadPageInfo() {
    this.cartItems = JSON.parse(localStorage.getItem(CART_ITEMS) || '[]');
    this._productIds = this.cartItems.map(x => x.productId);
    this._cartHttpService
      .getCartItem(this._productIds)
      .subscribe(res => this.items = res.products);
    this._shopHttpService
      .getShops()
      .subscribe(res => this.shops = res.shops);
  }

  delete(index: number) {
    if (index >= 0) {
      this.cartItems = JSON.parse(localStorage.getItem(CART_ITEMS) || '[]');
      this.cartItems.splice(index, 1);
      localStorage.setItem(CART_ITEMS, JSON.stringify(this.cartItems));
      this.loadPageInfo();
    }
  }

  onOrderClick() {
    if (this._authService.isAuthenticated())
      this.addOrder();
    else {
      this._authService.login();
      this._authService
        .isAutorise
        .subscribe(() => this.addOrder())
    }
  }

  addOrder() {
    this.totalPrice = 0;
    this.items.forEach(x => {
      this.totalPrice += (this.cartItems.find(y => y.productId === x.id)?.quantity ?? 1) * x.price;
    });
    const modalRef = this._modalService.open(OrderDashboardComponent, { centered: true });
    const module = <OrderDashboardComponent>modalRef.componentInstance;
    module.totalPrice = this.totalPrice;
    module.shops = this.shops;
    module.order.subscribe((item: Order) => {
      item.totalPrice = this.totalPrice;
      item.orderLines = this.cartItems;
      module.activeModal.close();
      this._orderHttpService.postOrder(item).subscribe(() => {
        localStorage.removeItem(CART_ITEMS);
        this.cartItems = [];
        this.items = [];
        this._router.navigate(['order']);
      });
    });
  }
}
