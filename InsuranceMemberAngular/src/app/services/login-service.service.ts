import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {

  _loginUrl="https://localhost:44312/api/LoginRegister/login-user";
  //_loginUrl="http://20.219.1.181/api/gateway/Login/login-user";
  usertype:string='';
  
  constructor(private http:HttpClient, private _router:Router) { }

  loginUser(login:any){
    return this.http.post<any>(this._loginUrl,login);
  }
  logginIn(){
    return !!localStorage.getItem('token');
  }

  IsAdmin(){
    if(localStorage.getItem('userRole') == "Admin"){
      return true;
    }else{
      return false;
    }
  }

  logoutUser(){
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    localStorage.removeItem('userType');
    localStorage.removeItem('userId');
    this._router.navigate(['login']);
    window.location.href="login";
    
  }


  getToken(){
    return localStorage.getItem('token');
  }
}
