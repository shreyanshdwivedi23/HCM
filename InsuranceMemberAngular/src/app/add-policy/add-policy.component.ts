import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { addpolicydata } from '../models/addpolicydata';
import { policystatusdata } from '../models/policystatusdata';
import { policytypedata } from '../models/policytypedata';

import { InsurancePolicyService } from '../services/insurance-policy.service';

@Component({
  selector: 'app-add-policy',
  templateUrl: './add-policy.component.html',
  styleUrls: ['./add-policy.component.css']
})
export class AddPolicyComponent {
  constructor(private _service:InsurancePolicyService,private _router:Router,private route: ActivatedRoute,public datepipe: DatePipe) { }
  addPolicyModel: addpolicydata = new addpolicydata();
  policyStatusList:Array<policystatusdata> = new Array<policystatusdata>();
  policyTypeList:Array<policytypedata> = new Array<policytypedata>();
  
  ErrorMessage:any='';
  isEdit:boolean=false;
  isAlert1=false;
  alertMessage="";
  btnHide:boolean=false;
  date:any;
  
  Success(data:any){
    if(data.errmsg==""){
      this.addPolicyModel = data.data;
    }else{
      this.ErrorMessage = data.errmsg;
    }
  }

  SuccessStatusList(data:any){
      console.log(data);
      this.policyStatusList = data;
  }

  SuccessTypeList(data:any){
      this.policyTypeList = data;
  }

  SuccessByIdDtls(data:any){
    console.log("see data")
    console.log(data);
    let EffectiveDate =this.datepipe.transform(data.data.policyEffectiveDate, 'yyyy-MM-dd')?.toString();
    this.addPolicyModel = data.data;
    this.addPolicyModel.policyEffectiveDate = EffectiveDate;
}

  AddInsurancePolicy(){
    if(this.addPolicyModel.memberId==0)
    {
      this.ErrorMessage = "Please enter member Id.";
      return;
    }
    if(this.addPolicyModel.policyPremiumAmount==0)
    {
      this.ErrorMessage = "Please enter premium amount greater than 0.";
      return;
    }
    if(this.addPolicyModel.policyStatusId ==0)
    {
      this.ErrorMessage = "Please select policy status.";
      return;
    }
    if(this.addPolicyModel.policyTypeId ==0)
    {
      this.ErrorMessage = "Please select policy type.";
      return;
    }
    if(this.addPolicyModel.policyEffectiveDate ==undefined)
    {
      this.ErrorMessage = "Please select policy effective date.";
      return;
    }
    
    
    debugger;
    this.addPolicyModel.policyStatusId = Number(this.addPolicyModel.policyStatusId)
    this.addPolicyModel.policyTypeId = Number(this.addPolicyModel.policyTypeId)
    this.addPolicyModel.memberId = Number(this.addPolicyModel.memberId)
    this.addPolicyModel.userId = Number(localStorage.getItem("userId"));
    this.addPolicyModel.policyPremiumAmount = Number(this.addPolicyModel.policyPremiumAmount)
    if(this.isEdit==false){
      this._service.AddInsurancePolicy(this.addPolicyModel).subscribe(res=>{
        if(res.message!=""){
          this.isAlert1=true;
          this.alertMessage=res.message;
          this.ErrorMessage="";
          
        }
        else{
          
          this.ErrorMessage=res.errmsg;
          
        }
        
      },res=>
      {
        console.log(res);
        this.ErrorMessage="Some error have occured";
        
      });
    }else{
      this._service.UpdateInsurancePolicy(this.addPolicyModel).subscribe(res=>{
        if(res.message!=""){
          this.isAlert1=true;
          this.alertMessage=res.message;
          this.ErrorMessage="";
          
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
  }
  
  GetPolicyStatus(){
    this._service.GetPolicyStatus().subscribe(res=>this.SuccessStatusList(res)
    ,res=>
    {
      console.log(res);
      this.ErrorMessage="Some error have occured";
    });
  }

  GetPolicyType(){
    this._service.GetPolicyType().subscribe(res=>this.SuccessTypeList(res)
    ,res=>
    {
      console.log(res);
      this.ErrorMessage="Some error have occured";
    });
  }

  GetAllMemberPolicyByPID(){
    this._service.GetAllMemberPolicyByPID(this.addPolicyModel).subscribe(res=>this.SuccessByIdDtls(res)
    ,res=>
    {
      console.log(res);
      this.ErrorMessage="Some error have occured";
    });
  }
  
  ngOnInit(): void {
    //debugger;
    let sub = this.route.params.subscribe(params => {
      //debugger;
      console.log(atob(params['mid']));
      
      if(params['mid']){
        this.addPolicyModel.memberId = Number(atob(params['mid']));
      }
      if(params['pid']){
        //debugger;
        this.addPolicyModel.policyId = Number(atob(params['pid']));
        this.isEdit=true;
      }
    });

    this.GetPolicyType();
    this.GetPolicyStatus();
    this.GetAllMemberPolicyByPID()
    this.date = new Date;
  }
}
