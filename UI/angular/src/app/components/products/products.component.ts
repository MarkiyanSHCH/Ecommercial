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

  constructor(
    private _productService: ProductService,
    private _activeRoute: ActivatedRoute,
    private _router: Router
  ) { }

  id: any | undefined;
  productList: Product[] = <Product[]>[];

  ngOnInit(): void {
    this._activeRoute.params.subscribe(params => {
      if (this._router.url.startsWith("/category")) {
        this.id = params.id;
        this.refreshProdListByCategory();
      }
      else {
        this.refreshProdList();
      }
    });
  }

  refreshProdList() {
    this._productService.getProducts().subscribe(data => {
      this.productList = data.products;
    });
  }

  refreshProdListByCategory() {
    this._productService.getProductByCategory(this.id).subscribe(data => {
      this.productList = data.products;
    });
  }

  routingTo(productId: Product) {
    this._router.navigate([`product/${productId}`])
  }

}
