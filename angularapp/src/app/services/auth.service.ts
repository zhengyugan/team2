import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl:string = "https://localhost:44308/api/Users/"
  constructor(private http : HttpClient) { }

  signUp(userObj:any){
    return this.http.post<any>(`${this.baseUrl}register`,userObj);
  }

  login(userObj:any){
    return this.http.post<any>(`${this.baseUrl}authenticate`,userObj);

  }
}
