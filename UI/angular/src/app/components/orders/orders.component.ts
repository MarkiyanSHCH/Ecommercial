import { Component, OnInit } from '@angular/core';

import { forkJoin } from 'rxjs';

import { Order } from 'src/app/models/order/order';
import { OrderLine } from 'src/app/models/order/orderLine';
import { Shop } from 'src/app/models/shop/shop';
import { OrderHttpService } from 'src/app/services/http/order.http.service';
import { ShopHttpService } from 'src/app/services/http/shop.http.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  orders: Order[] = <Order[]>[];
  orderLinesMap: Map<number, OrderLine[]> = new Map<number, OrderLine[]>();
  shops: Shop[] = <Shop[]>[];

  constructor(
    private _orderHttpService: OrderHttpService,
    private _shopHttpService: ShopHttpService
  ) {
    this.orderLinesMap.clear();
  }

  ngOnInit(): void {
    forkJoin({
      orderList: this._orderHttpService
        .getOrders(),
      shopList: this._shopHttpService
        .getShops()
    }).subscribe(({ orderList, shopList }) => {
      this.orders = orderList.orders;
      this.shops = shopList.shops;
    });
  }

  getShopByShopId(shopId: number): Shop {
    return this.shops.find(x => x.id === shopId) ?? <Shop>{}
  }

  getOrderLines(orderId: number) {
    if (!this.orderLinesMap.get(orderId))
      this._orderHttpService
        .getOrderLines(orderId)
        .subscribe(res => this.orderLinesMap.set(orderId, res.orderLines));
  }
}
