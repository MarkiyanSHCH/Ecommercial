import { Component, Input } from '@angular/core';

import { OrderLine } from 'src/app/models/order/orderLine';

@Component({
  selector: 'app-order-lines',
  templateUrl: './order-lines.component.html',
  styleUrls: ['./order-lines.component.css']
})
export class OrderLinesComponent {

  @Input() orderLine: OrderLine = <OrderLine>{};
}
