import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Product } from 'src/app/models/Product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  Product: Product = <Product>{};

  constructor(
    private _productService: ProductService,
    private _activeRoute: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this._productService
    .getProductById(Number(
      this._activeRoute.snapshot.paramMap.get("id")
      ))
    .subscribe(res => this.Product = res);
  }
}
