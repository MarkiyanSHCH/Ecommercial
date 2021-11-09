import { Component, EventEmitter, Input, Output } from '@angular/core';

import { Order } from 'src/app/models/order/order';
import { Shop } from 'src/app/models/shop/shop';

@Component({
  selector: 'app-order-collapse',
  templateUrl: './order-collapse.component.html',
  styleUrls: ['./order-collapse.component.css']
})
export class OrderCollapseComponent {

  expanded = false;

  @Input() order: Order = <Order>{};
  @Input() shop: Shop = <Shop>{};
  @Output() expand: EventEmitter<number> = new EventEmitter<number>();

  toggle(orderNumber: number): void {
    this.expanded = !this.expanded;
    if (this.expanded) this.expand.next(orderNumber);
  }
}
