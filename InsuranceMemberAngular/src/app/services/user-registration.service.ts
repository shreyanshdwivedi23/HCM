import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserRegistrationService {

  _registerUrl="https://localhost:44312/api/LoginRegister/register-user";
  //_registerUrl="http://20.219.1.181/api/gateway/Login/register-user";
  constructor(private http:HttpClient) { }

  registerUser(login:any){
    console.log("login model -->", login)
    return this.http.post<any>(this._registerUrl,login);
  }
  getToken(){
    return localStorage.getItem('token');
  }
}
