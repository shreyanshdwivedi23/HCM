import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddPolicyComponent } from './add-policy/add-policy.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { MemberRegistrationComponent } from './member-registration/member-registration.component';
import { MembersearchComponent } from './membersearch/membersearch.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [];

@NgModule({
  imports: [  RouterModule.forRoot([
    {path: 'home', component: HomeComponent},
    {path: '', component: HomeComponent},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'registerMember/:un/:pa', component: MemberRegistrationComponent},
    {path: 'memberSearch', component: MembersearchComponent},
    {path: 'addPolicy/:mid', component: AddPolicyComponent},
    {path: 'addPolicy/:mid/:pid', component: AddPolicyComponent}
  ]),],
  exports: [RouterModule]
})
export class AppRoutingModule { }
