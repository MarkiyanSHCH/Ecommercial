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

  id: number = <number>{};
  Product: Product = <Product>{};

  constructor(private service:ProductService, 
    private activeRoute: ActivatedRoute, 
    private changedetectect: ChangeDetectorRef
    ) {}

  ngOnInit(): void {
      this.activeRoute.params.subscribe(params =>{
      this.id = params.id;
      this.refreshProdList();
      this.changedetectect.detectChanges();
    });
  }
  refreshProdList(){
   this.service.getProductById(this.id).subscribe(res => this.Product = res);
  }
}
