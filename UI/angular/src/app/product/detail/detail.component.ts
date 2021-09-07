import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  id!: number;
  Product:any;

  constructor(private service:SharedService, private activeRoute: ActivatedRoute, private changedetectect: ChangeDetectorRef) {

  }

  

  ngOnInit(): void {
    this.activeRoute.params.subscribe(async params =>{
      this.id = params.id;
      await this.refreshProdList();
      this.changedetectect.detectChanges();
    });
    

  }

  async refreshProdList(){
   this.Product =  await this.service.getProductById(this.id);
  }

}
