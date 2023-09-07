import { Component, EventEmitter, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public accountService : AccountService) {
   
   }

  title = 'CVapp';
  vacanciesTitle= "Вакансии";
  cvTitle= "Моё резюме";

  model: any ={};
  loggedIn = false;


  ngOnInit(): void {
  }

}
