import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginServiceService } from './login-service.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthguardServiceService {

  constructor(private _auth:LoginServiceService,private _router:Router,private jwt: JwtHelperService) { }
  name ='';
  Id = 0;
  role='';
  mId=0;
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if(this._auth.logginIn()){
      return true;
    }
    else{
      this._router.navigate(['']);
      return false;
    }
  }

  getCurrentUserId():number{
    this.Id=this.jwt.decodeToken(this._auth.getToken()?.toString()).nameid;
    console.log(this.jwt.decodeToken(this._auth.getToken()?.toString()));
    return Number(this.Id);
  }

  getCurrentMemberId():number{
    this.mId=this.jwt.decodeToken(this._auth.getToken()?.toString()).email;
    console.log(this.jwt.decodeToken(this._auth.getToken()?.toString()));
    return Number(this.mId);
  }

  getCurrentUserName():string{
    this.name=this.jwt.decodeToken(this._auth.getToken()?.toString()).unique_name;
    return this.name;
  }

  getCurrentUserType():string{
    this.role=this.jwt.decodeToken(this._auth.getToken()?.toString()).role;
    return this.role;
  }
}
