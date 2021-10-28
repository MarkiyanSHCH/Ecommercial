import { Component, OnInit } from '@angular/core';

import { Order } from 'src/app/models/order/order';
import { OrderLine } from 'src/app/models/order/orderLine';
import { OrderHttpService } from 'src/app/services/http/order.http.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  orders: Order[] = <Order[]>[];
  orderLines: OrderLine[] = <OrderLine[]>[];

  constructor(private _orderHttpService: OrderHttpService) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this._orderHttpService
      .getOrders()
      .subscribe(res => this.orders = res.orders);
  }

  getOrderLines(orderId: number) {
    this._orderHttpService
      .getOrderLines(orderId)
      .subscribe(res => this.orderLines = res.orderLines);
  }
}
