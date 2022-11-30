import { DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthguardServiceService } from './services/authguard.service.service';
import { LoginServiceService } from './services/login-service.service';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { MemberRegistrationComponent } from './member-registration/member-registration.component';
import { NgToastModule } from 'ng-angular-popup';
import { MembersearchComponent } from './membersearch/membersearch.component';
import { AddPolicyComponent } from './add-policy/add-policy.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    HeaderComponent,
    MemberRegistrationComponent,
    MembersearchComponent,
    AddPolicyComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgToastModule
  ],
  providers: [LoginServiceService, DatePipe, AuthguardServiceService,{provide:JWT_OPTIONS,useValue:JWT_OPTIONS},JwtHelperService],
  bootstrap: [AppComponent]
})
export class AppModule { }
