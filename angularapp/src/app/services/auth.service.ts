import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl: string = "https://team2webapp001.azurewebsites.net/api/Users/"
  constructor(private http : HttpClient, private router: Router) { }

  signUp(userObj:any){
    return this.http.post<any>(`${this.baseUrl}register`,userObj);
  }

  signOut(){
    localStorage.clear();
    this.router.navigate(['login'])
  }

  login(userObj:any){
    return this.http.post<any>(`${this.baseUrl}authenticate`,userObj);
  }

  storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue)
  }

  getToken(){
    return localStorage.getItem('token')
  }

  isLoggedIn(): boolean{
    return !!localStorage.getItem('token')
  }

  storeRole(roleValue: string){
    localStorage.setItem('role', roleValue)
  }

  getRole(){
    return localStorage.getItem('role')
  }
  
}
