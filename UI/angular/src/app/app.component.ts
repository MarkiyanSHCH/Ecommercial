import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { ACCESS_TOKEN_KEY, AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Task';

  constructor(private service:SharedService, private _authService: AuthService) { }

  public get isLoggedIn(): boolean{
    return this._authService.isAuthenticated()
  }

  login(email:string,password:string){
    console.log(localStorage.key(0))
    this._authService.login(email,password)
      .subscribe(res => {

      }, error =>{
        alert("Wrong login or password")
      })
  }

  logout(){
    this._authService.logout()
  }
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
