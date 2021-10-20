import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Product } from 'src/app/models/Product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  productList: Product[] = <Product[]>[];

  constructor(
    private _productService: ProductService,
    private _activeRoute: ActivatedRoute,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this._activeRoute.params.subscribe(params => {
      if (this._router.url.startsWith("/category"))
        this._productService.getProductByCategory(params.id).subscribe(data => {
          this.productList = data.products;
        });
      else
        this._productService.getProducts().subscribe(data => {
          this.productList = data.products;
        });
    });
  }

  getCover(product: Product): string {
    return 'url(' + product.photoFileName + ')';
  }
}
