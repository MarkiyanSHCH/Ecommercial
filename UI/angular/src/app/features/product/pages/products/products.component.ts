import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { finalize } from 'rxjs/operators';

import { Loadable, Product } from 'src/app/models';
import { ProductHttpService } from 'src/app/services/http/product.http.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent extends Loadable implements OnInit {

  productList: Product[] = <Product[]>[];

  constructor(
    private _productHttpService: ProductHttpService,
    private _activeRoute: ActivatedRoute,
    private _router: Router
  ) {
    super();
  }

  ngOnInit(): void {
    this.enableLoading();
    this._activeRoute.params.subscribe(params => {
      if (this._router.url.startsWith("/category"))
        this._productHttpService.getProductByCategory(params.id)
          .pipe(finalize(() => this.disableLoading()))
          .subscribe(data => {
            this.productList = data.products;
          },
            () => this._router.navigate(['not-found'])
          );
      else
        this._productHttpService.getProducts()
          .pipe(finalize(() => this.disableLoading()))
          .subscribe(data => {
            this.productList = data.products;
          },
            () => this._router.navigate(['not-found'])
          );
    });
  }
}
