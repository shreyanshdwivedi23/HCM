import { Component } from '@angular/core';
import { LoginServiceService } from '../services/login-service.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  constructor(private _auth:LoginServiceService) { }

  ngOnInit(): void {
    
  }

  LoggedIn(Input:boolean):boolean{
    if(Input){
      return this._auth.logginIn();
    }
    else{
      return !this._auth.logginIn();
    }
  }

  IsAdmin(Input:boolean):boolean{
    if(Input){
      return this._auth.IsAdmin();
    }
    else{
      return !this._auth.IsAdmin();
    }
  }
  Logout(){
    this._auth.logoutUser();
    
  }
  
  userType = localStorage.getItem('userType');
  userName = localStorage.getItem('userName');
  
}
