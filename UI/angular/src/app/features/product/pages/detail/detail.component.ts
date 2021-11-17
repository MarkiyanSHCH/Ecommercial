import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Property } from 'src/app/models/product/property';
import { Product } from 'src/app/models/product/product';
import { CartItem } from 'src/app/models/cart/cartItem';
import { CartService } from 'src/app/services/cart.service';
import { ProductHttpService } from 'src/app/services/http/product.http.service';
import { CartModuleComponent } from 'src/app/module/cart-dashboard/cart-dashboard.component';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  product: Product = <Product>{
    properties: <Property[]>[]
  };

  constructor(
    private _modalService: NgbModal,
    private _productHttpService: ProductHttpService,
    private _cartService: CartService,
    private _activeRoute: ActivatedRoute,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this._productHttpService
      .getProductById(Number(
        this._activeRoute.snapshot.paramMap.get("id")
      ))
      .subscribe(
        res => this.product = res,
        () => this._router.navigate(['not-found'])
      );
  }

  addCartProduct() {
    const modalRef = this._modalService.open(CartModuleComponent, { centered: true });
    const module = <CartModuleComponent>modalRef.componentInstance;
    module.product = this.product;
    module.cartItem.subscribe((item: CartItem) => {
      module.activeModal.close();
      this._cartService.addItemCart(item.productId, item.quantity, item.note);
    });
  }
}
