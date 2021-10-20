import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Product } from 'src/app/models/Product';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  id: number = 0;
  product: Product = <Product>{};

  constructor(
    private _productService: ProductService,
    private _orderService: OrderService,
    private _activeRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.id = Number(this._activeRoute.snapshot.paramMap.get("id"))
    this._productService
      .getProductById(this.id)
      .subscribe(res => this.product = res);
  }

  addOrderProduct() {
    this._orderService.createOrder(this.id).subscribe();
  }
}
