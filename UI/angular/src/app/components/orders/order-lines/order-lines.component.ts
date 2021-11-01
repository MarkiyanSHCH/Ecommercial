import { Component, Input, OnInit } from '@angular/core';
import { OrderLine } from 'src/app/models/order/orderLine';
import { OrderHttpService } from 'src/app/services/http/order.http.service';

@Component({
  selector: 'app-order-lines',
  templateUrl: './order-lines.component.html',
  styleUrls: ['./order-lines.component.css']
})
export class OrderLinesComponent {

  @Input() orderLines: OrderLine[] = <OrderLine[]>[];
}
