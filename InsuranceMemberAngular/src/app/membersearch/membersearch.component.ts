import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MemberData } from '../models/memberdata';
import { policystatusdata } from '../models/policystatusdata';
import { searchdata } from '../models/search';
import { InsurancePolicyService } from '../services/insurance-policy.service';
import { MemberRegistrationService } from '../services/member-registration.service';

@Component({
  selector: 'app-membersearch',
  templateUrl: './membersearch.component.html',
  styleUrls: ['./membersearch.component.css']
})
export class MembersearchComponent {
  constructor(private http:HttpClient,private _service:InsurancePolicyService,private _router:Router) { }
  memberModel: searchdata = new searchdata();
  memberModels: Array<searchdata> = new Array<searchdata>();
  memberId:any;
  userType:any;
  ErrorMessage:any='';
  isEdit:boolean=false;
  isHide:boolean=false;
  policyStatusList:Array<policystatusdata> = new Array<policystatusdata>();
  Success(input:any){
    //debugger;
    console.log("search -->");
    console.log(input);
    this.ErrorMessage="";
    this.memberModels = input;
  }

  GetPolicyStatus(){
    this._service.GetPolicyStatus().subscribe(res=>{res
      this.policyStatusList = res;
    }
    ,res=>
    {
      console.log(res);
      this.ErrorMessage="Some error have occured";
    });
  }

  searchMemberPolicy(){
    console.log("in search---->");
    console.log(this.memberModel);
    debugger;
    this.memberModel.policyTypeId = Number(this.memberModel.policyTypeId);
    this.memberModel.policyStatusId = Number(this.memberModel.policyStatusId);
    this.memberModel.memberId = Number(this.memberModel.memberId);
    this.memberModel.policyId = Number(this.memberModel.policyId);
    
    this._service.memberSearchAll(this.memberModel).subscribe(res=>this.Success(res)
    ,res=>
    {
      console.log(res);
      this.ErrorMessage="Some error have occured";
    });
  }
  IsAdmin(){
    
  }

  AddNewPolicy(){
    this.memberId = localStorage.getItem('mId');
    this._router.navigate(['addPolicy', btoa(this.memberId)]);
  }
  // for admin
  AddPolicy(memberId:any){
      debugger;
      console.log("memberId ->" + memberId);
      console.log(btoa(memberId));
      this._router.navigate(['addPolicy', btoa(memberId)]);
  
  }

  UpdatePolicy(memberId:any,policyId:any){
    debugger;
    console.log("memberId ->" + memberId);
    console.log(btoa(memberId));
    this._router.navigate(['addPolicy', btoa(memberId), btoa(policyId)]);

}
  
  ngOnInit(): void {
    //debugger;
    this.searchMemberPolicy();
    this.GetPolicyStatus();
    this.userType= localStorage.getItem('userType');
    if(this.userType=="Admin"){
      this.isHide = true;
    }
    else{
      this.isHide = false;
    }
     
  }
}
