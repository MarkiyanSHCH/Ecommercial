import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Task';

  constructor(private service:SharedService) { }

  CategoriesList:any=[];

  ngOnInit(): void {
      this.refreshProdList();
  }

  refreshProdList(){
    this.service.getCategory().subscribe(data=>{
      this.CategoriesList=data;
    });
  }

}
