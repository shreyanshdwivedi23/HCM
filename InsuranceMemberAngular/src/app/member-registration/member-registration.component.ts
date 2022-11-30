import { Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MemberRegistrationService } from '../services/member-registration.service';
import { MemberData } from '../models/memberdata';

@Component({
  selector: 'app-member-registration',
  templateUrl: './member-registration.component.html',
  styleUrls: ['./member-registration.component.css']
})
export class MemberRegistrationComponent {
  constructor(private _service:MemberRegistrationService,private _router:Router,private route: ActivatedRoute) { }
  ErrorMessage:any='';
  MemberDataModel:MemberData=new MemberData();
  isAlert1=false;
  alertMessage="";
  date:any;
  expression: RegExp = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
  ///^(?=.{1,254}$)(?=.{1,64}@)[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+(\.[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+)*@[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?(\.[A-Za-z0-9]([A-Za-z0-9-]{0,61}[A-Za-z0-9])?)*$/;
  email: string = 'john@gmail.com';
  
  ngOnInit(): void {
    //this.MemberDataModel.memberUsername = this._router.params.subscribe((params: Params) => this.myParam = params['caller']);
    let sub = this.route.params.subscribe(params => {
      //debugger;
      console.log(atob(params['un']));
      console.log(atob(params['pa']));
      if(params['un']){
        //debugger;
        this.MemberDataModel.memberUsername = atob(params['un']);
        this.MemberDataModel.memberPassword = atob(params['pa']);
        console.log("book id passed " + this.MemberDataModel.memberUsername)
      }
    });
    this.date= new Date;
  }

  registerUser(){
    if(this.MemberDataModel.memberFirstName==""){
      this.ErrorMessage="Please enter first name.";
      return;
    }
    if(this.MemberDataModel.memberLastName==""){
      this.ErrorMessage="Please enter last name.";
      return;
    }
    if(this.MemberDataModel.memberEmail==""){
      
      this.ErrorMessage="Please enter email.";
      return;
    }
    if(this.MemberDataModel.memberEmail!=""){
      
      if(!this.expression.test(this.MemberDataModel.memberEmail)){
      this.ErrorMessage="Please enter valid email.";
      return;
      }
    }
    if(this.MemberDataModel.memberAddress==""){
      this.ErrorMessage="Please enter address.";
      return;
    }
    if(this.MemberDataModel.memberState==""){
      this.ErrorMessage="Please enter state.";
      return;
    }
    if(this.MemberDataModel.memberCity==""){
      this.ErrorMessage="Please enter city.";
      return;
    }
    debugger;

    if(this.MemberDataModel.memberDateOfBirth==undefined){
      this.ErrorMessage="Please select Date of birth.";
      return;
    }
    if(this.MemberDataModel.memberDateOfBirth!=undefined){
      // var ToDate = new Date();
      // if (new Date(this.MemberDataModel.memberDateOfBirth).getTime() <= ToDate.getTime()) {
      // this.ErrorMessage="Please select date before todays date.";
      // return;
      // }
      
    }
    //this.MemberDataModel.isRegister=true;
    this._service.registerUser(this.MemberDataModel).subscribe(res=>{
      if(res.message!=""){
        this.isAlert1=true;
        this.alertMessage=res.message;
        //this.ErrorMessage=res.message;
        
        setTimeout(() => {
      this._router.navigate(['login']);
    }
    , 5000);
      }
      else{
        this.isAlert1=false;
        this.ErrorMessage=res.errmsg;
        
      }
    },res=>
    {
      console.log(res);
      this.ErrorMessage="Some error have occured";
      document.getElementById('btnErrorMsg')?.click();
    });
  }


}
