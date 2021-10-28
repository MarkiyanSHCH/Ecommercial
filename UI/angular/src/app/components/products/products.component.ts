import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Product } from 'src/app/models/product/product';
import { ProductHttpService } from 'src/app/services/http/product.http.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  productList: Product[] = <Product[]>[];

  constructor(
    private _productHttpService: ProductHttpService,
    private _activeRoute: ActivatedRoute,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this._activeRoute.params.subscribe(params => {
      if (this._router.url.startsWith("/category"))
        this._productHttpService.getProductByCategory(params.id).subscribe(data => {
          this.productList = data.products;
        });
      else
        this._productHttpService.getProducts().subscribe(data => {
          this.productList = data.products;
        });
    });
  }
}
