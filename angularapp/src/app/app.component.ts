import { Component } from '@angular/core';

@Component({
  selector: 'pm-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.css']
  // template: `
  // <nav class="navbar navbar-expand navbar-light bg-light">
  //   <a class="navbar-brand" style="margin-left: 10px;">{{pageTitle}}</a>
  //   <ul class="nav nav-pills">
  //     <li><a class="nav-link" routerLink="/welcome">Home</a></li>
  //     <li><a class="nav-link" routerLink="/admin/products">Product List</a></li>
  //   </ul>
  // </nav>
  // <div class="container">
  //   <router-outlet></router-outlet>
  // </div>
  // `
})
export class AppComponent {
  pageTitle: string = 'Team 2 Shop';
  title: any;
}
