import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserData } from '../models/userdata';
import { AuthguardServiceService } from '../services/authguard.service.service';
import { LoginServiceService } from '../services/login-service.service';
import { NgToastService } from 'ng-angular-popup';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  userId = 0;
  userName = '';
  userType = '';
  mId=0;
  constructor(private toast: NgToastService,private _service:LoginServiceService,private _router:Router, private _authservice:AuthguardServiceService) { }
  ErrorMessage:any='';
  UserDataModel:UserData=new UserData();
  isAlert=false;
  alertMessage="";
  
  ngOnInit(): void {
    
  }
  showSuccess(msg:any) {
    alert(msg);
    this.toast.success({detail:"SUCCESS",summary:msg,duration:5000});
  }
  
  showError(err:any) {
    this.toast.error({detail:"ERROR",summary:err,duration:5000});
  }

  loginUser(){
    if(this.UserDataModel.userName=="")
    {
      //alert("Please enter user name.");
      this.ErrorMessage = "Please enter user name.";
      this.showError(this.ErrorMessage);
      return;
    }
    if(this.UserDataModel.userPassword=="")
    {
      this.ErrorMessage = "Please enter password.";
      this.showError(this.ErrorMessage);
      return;
    }
    

    this._service.loginUser(this.UserDataModel).subscribe(res=>{
      
      console.log(res);
      if(res.message !="" || res.message !=null){
        this.ErrorMessage=res.message;
      }
      
      localStorage.setItem('token',res.token);
      //console.log(res.user);
      if(res.token !="" && res.token !=null){
        debugger;
      this.userId = this._authservice.getCurrentUserId();
      this.userName = this._authservice.getCurrentUserName();
      this.userType = this._authservice.getCurrentUserType();
      this.mId = this._authservice.getCurrentMemberId();
      
      localStorage.setItem('userType',this.userType);
      localStorage.setItem('userName',this.userName);
      localStorage.setItem('userId',this.userId.toString());
      localStorage.setItem('mId',this.mId.toString());
      debugger;
      this.isAlert=true;
      this.alertMessage = "Login Successfull";
      
      window.location.href="memberSearch";
      this._router.navigate(['memberSearch']);
      //window.location.reload();
      }
      
    },res=>
    {
      console.log(res);
      if(res.message !="" && res.message !=null){
        this.ErrorMessage=res.message;
      }
      else{
        this.ErrorMessage="Some error has occurred!";
      }
      
      document.getElementById('btnErrorMsg')?.click();
    });
  }
}
