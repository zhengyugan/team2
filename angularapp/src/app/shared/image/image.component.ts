import { Component } from '@angular/core';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css']
})
export class ImageComponent {
  imageURL = 'https://team2webapp001.azurewebsites.net/api/images/Picture1.png';
}
