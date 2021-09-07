import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  constructor(private service:SharedService,private activeRoute: ActivatedRoute, private router:Router) { }

  id!: number;
  ProductList:any=[];

  ngOnInit(): void {
    this.activeRoute.params.subscribe(params =>{
      if(this.router.url.startsWith("/product/category")){
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
      this.ProductList=data;
    });
  }

  refreshProdListByCategory(){
    this.service.getProductByCategory(this.id).subscribe(data=>{
      this.ProductList=data;
    });
  }
  
  routingTo(productId: any){
    this.router.navigate([`product/${productId}`])
  }

}
