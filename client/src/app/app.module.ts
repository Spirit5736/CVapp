import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { SignInComponent } from './sign-in/sign-in.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule } from '@angular/forms';
import { AccountService } from './_services/account.service';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { CvComponent } from './cv/cv.component';

@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    RegisterComponent,
    HomeComponent,
    NavComponent,
    CvComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbNavModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
