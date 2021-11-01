import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { Order } from 'src/app/models/order/order';
import { Shop } from 'src/app/models/shop/shop';

@Component({
  selector: 'app-order-dashboard',
  templateUrl: './order-dashboard.component.html',
  styleUrls: ['./order-dashboard.component.css']
})
export class OrderDashboardComponent {

  model: Order = <Order>{};
  shopsModal: Shop[] = <Shop[]>[];

  @Input() set shops(item: Shop[]) {
    this.shopsModal = item;
  }
  @Input() totalPrice = 0;
  @Output() order = new EventEmitter<Order>();

  constructor(public activeModal: NgbActiveModal) { }
}
