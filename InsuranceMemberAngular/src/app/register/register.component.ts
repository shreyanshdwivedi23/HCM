import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserData } from '../models/userdata';
import { UserRegistrationService } from '../services/user-registration.service';
import { delay, map,  tap } from "rxjs/operators";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(private _service:UserRegistrationService,private _router:Router) { }
  ErrorMessage:any='';
  UserDataModel:UserData=new UserData();
  password="";
  username = "";
  isAlert1=false;
  alertMessage="";
  expression: RegExp = /^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8}$/;
  ngOnInit(): void {
  }

  registerUser(){
    if(this.UserDataModel.userName=="")
    {
      this.ErrorMessage = "Please enter user name.";
      return;
    }
    if(this.UserDataModel.userPassword=="")
    {
      this.ErrorMessage = "Please enter password.";
      return;
    }
    if(this.UserDataModel.userPassword!=""){
      
      if(!this.expression.test(this.UserDataModel.userPassword)){
      this.ErrorMessage="Please enter strong password including length of 8, Upper case , lower case & special letter,digits. eg:Abcd@123";
      return;
      }
    }
    if(this.UserDataModel.userConfirmPassword!=this.UserDataModel.userPassword)
    {
      this.ErrorMessage = "Password do not match.";
      return;
    }
    if(this.UserDataModel.userRole =="")
    {
      this.ErrorMessage = "Please select user role.";
      return;
    }
    
    if(this.UserDataModel.userRole == "Admin"){

    this.UserDataModel.isRegister=true;
    this._service.registerUser(this.UserDataModel).subscribe(res=>{
      if(res.message!=""){
        this.isAlert1=true;
        this.alertMessage=res.message;
        this.ErrorMessage="";
        
        setTimeout(() => {
      this._router.navigate(['login']);
    }
    , 5000);
      }
      else{
        
        this.ErrorMessage=res.errmsg;
        
      }
      
    },res=>
    {
      console.log(res);
      this.ErrorMessage="Some error have occured";
      
    });
  }
  else{
    let un = this.UserDataModel.userName;
    let pa = this.UserDataModel.userPassword;
    this._router.navigate(['/registerMember', btoa(un), btoa(pa)]);
  }
  }

}
