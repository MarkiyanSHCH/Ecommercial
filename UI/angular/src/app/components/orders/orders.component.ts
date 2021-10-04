import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/Product';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  constructor(private os: OrderService) { }

  orders: Product[] = []

  ngOnInit(): void {
    this.os.getOrders()
      .subscribe(res =>{
        this.orders = res
      })
  }

}
