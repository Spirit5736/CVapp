import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';
import { BehaviorSubject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private loggedIn = false;

  get isButtonClicked(): boolean {
    return this.loggedIn;
  }

  setButtonClicked() {
    this.loggedIn = true;
  }


  baseUrl = 'https://localhost:7063/api/';
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post<User>(`${this.baseUrl}user/login`, model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
     )
    }

    register(model : any) {
      return this.http.post(`${this.baseUrl}user/register`, model);
    }

    setCurrentUser(user : User) {
      this.currentUserSource.next(user);
    }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    }
   
}
