import { Component, Input } from '@angular/core';

import { OrderLine } from 'src/app/models/order/orderLine';

@Component({
  selector: 'app-order-line',
  templateUrl: './order-line.component.html',
  styleUrls: ['./order-line.component.css']
})
export class OrderLineComponent {

  @Input() line: OrderLine = <OrderLine>{};
}
