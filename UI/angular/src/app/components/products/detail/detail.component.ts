import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CartItem } from 'src/app/models/cart/cartItem';

import { Product } from 'src/app/models/product/product';
import { CartModuleComponent } from 'src/app/module/cart-dashboard/cart-dashboard.component';

import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/http/product.http.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  product: Product = <Product>{};

  constructor(
    private _modalService: NgbModal,
    private _productService: ProductService,
    private _cartService: CartService,
    private _activeRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this._productService
      .getProductById(Number(
        this._activeRoute.snapshot.paramMap.get("id")
      ))
      .subscribe(res => this.product = res);
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
