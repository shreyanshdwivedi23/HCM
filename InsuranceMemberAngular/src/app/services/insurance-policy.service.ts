import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class InsurancePolicyService {

  _memberSearchAllUrl = "https://localhost:44312/api/InsuranceMember/GetAllMemberPolicy";
  _addPolicyUrl="https://localhost:44312/api/InsuranceMember/AddInsurancePolicy";
  _updatePolicyUrl="https://localhost:44312/api/InsuranceMember/UpdateInsuranceolicy";
  
  _getPolicyByIdUrl="https://localhost:44312/api/InsuranceMember/GetAllMemberPolicyByPID";
  _polictStatusUrl="https://localhost:44312/api/InsuranceMember/GetPolicyStatus";
  _policyTypeUrl ="https://localhost:44312/api/InsuranceMember/GetPolicyType";
  // _getMyBooksUrl = "http://20.219.1.181/api/gateway/Reader/getMyBooks";
  // _refundBooksUrl = "http://20.219.1.181/api/gateway/Reader/refundBook?";
 // _memberSearchAllUrl = "http://20.219.1.181/api/gateway/Reader/memberSearchAll"
  userId = 0;
  constructor(private http:HttpClient) { }
  memberSearchAll(memberdata:any){
    //console.log("login model -->", book)
    return this.http.post<any>(this._memberSearchAllUrl, memberdata);
  }

  AddInsurancePolicy(data:any){
    console.log(data);
    return this.http.post<any>(this._addPolicyUrl, data);
  }
  UpdateInsurancePolicy(data:any){
    console.log(data);
    return this.http.post<any>(this._updatePolicyUrl, data);
  }
  GetAllMemberPolicyByPID(data:any){
    console.log(data);
    return this.http.post<any>(this._getPolicyByIdUrl, data);
  }
  GetPolicyStatus(){
    return this.http.get<any>(this._polictStatusUrl);
  }
  GetPolicyType(){
    return this.http.get<any>(this._policyTypeUrl);
  }
}
