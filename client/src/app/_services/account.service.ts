import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:7063/api/'

  constructor(private http: HttpClient) {}
    login(model : any) {
      return this.http.post(`${this.baseUrl}user/login`, model)
    }
   
}
