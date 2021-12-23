import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { forkJoin } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { CartItem } from 'src/app/models/cart/cartItem';
import { Loadable } from 'src/app/models/loadable';
import { Order } from 'src/app/models/order/order';
import { Product } from 'src/app/models/product/product';
import { Shop } from 'src/app/models/shop/shop';
import { OrderDashboardComponent } from 'src/app/module/order-dashboard/order-dashboard.component';
import { AuthService } from 'src/app/services/auth.service';
import { CartHttpService } from 'src/app/services/http/cart.http.service';
import { OrderHttpService } from 'src/app/services/http/order.http.service';
import { ShopHttpService } from 'src/app/services/http/shop.http.service';

export const CART_ITEMS = ('CartItemArray');

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent extends Loadable implements OnInit {

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
  ) {
    super();
  }

  ngOnInit() {
    this.loadPageInfo();
  }

  getProductByProductId(productId: number): Product {
    return this.items.find(x => x.id === productId) ?? <Product>{};
  }

  loadPageInfo() {
    this.enableLoading();
    this.cartItems = JSON.parse(localStorage.getItem(CART_ITEMS) || '[]');
    this._productIds = this.cartItems.map(x => x.productId);
    forkJoin({
      cartItem: this._cartHttpService
        .getCartItem(this._productIds),
      shopList: this._shopHttpService
        .getShops()
    }).pipe(finalize(() => this.disableLoading()))
      .subscribe(({ cartItem, shopList }) => {
        this.items = cartItem.products;
        this.shops = shopList.shops;
      });
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
        .subscribe(() => this.addOrder());
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
        this._router.navigate(['account/order']);
      });
    });
  }
}
