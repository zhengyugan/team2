import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'pm-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  // isLoggedIn = false;

  constructor( private auth: AuthService){}

  loggedin(){
    return localStorage.getItem('token');
  }
  
  logout(){
    this.auth.signOut()
  }
}

