import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  
  
  model: any ={};
  loggedIn = false;
  registerForm = false;

  constructor(private accountService : AccountService, private router: Router) {
   }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  login(){
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
        this.loggedIn = true;
        this.accountService.setButtonClicked();
        this.router.navigate(['/cv']);
      },
      error: error => console.log(error)      
    });
  }

  getCurrentUser(){
    this.accountService.currentUser$.subscribe({
      next : user => this.loggedIn = !!user,
      error : error => console.log(error)
    })
  }

  logout(){
    this.loggedIn = false;
    this.accountService.logout();
  }

  register() {
    this.router.navigate(['/register'])
  }

}
