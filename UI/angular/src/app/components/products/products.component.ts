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

  constructor(private service:ProductService,
    private activeRoute: ActivatedRoute, 
    private router:Router
    ) { }

  id: number = <number>{};
  productList: Product[] = <Product[]>{};

  ngOnInit(): void {
    this.activeRoute.params.subscribe(params =>{
      if(this.router.url.startsWith("/category")){
        this.id = params.id;
        this.refreshProdListByCategory();
      }
      else{
        this.refreshProdList();
      }
    });
  }

  refreshProdList(){
    this.service.getProducts().subscribe(data=>{
      this.productList=data.productList;
    });
  }

  refreshProdListByCategory(){
    this.service.getProductByCategory(this.id).subscribe(data=>{
      this.productList=data.productList;
    });
  }
  
  routingTo(productId: Product){
    this.router.navigate([`product/${productId}`])
  }
}
