import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from './product';

@Component({
  selector: 'pm-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {
  pageTitle: string = "Product Detail";
  product: IProduct | undefined;
  promotedProduct : IProduct[] | undefined;

  form = new FormGroup({  
    website: new FormControl('', Validators.required)  
  });  

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get("id"));
    this.pageTitle += `: ${id}`;
    this.product = {
      "productId": id,
      "productName": "Leaf Rake",
      "productCode": "GDN-0011",
      "releaseDate": "March 19, 2021",
      "description": "Leaf rake with 48-inch wooden handle.",
      "price": 19.95,
      "starRating": 3.2,
      "imageUrl": "assets/images/leaf_rake.png"
    }
    this.promotedProduct = [{
      "productId": id,
      "productName": "Leaf Rake",
      "productCode": "GDN-0011",
      "releaseDate": "March 19, 2021",
      "description": "Leaf rake with 48-inch wooden handle.",
      "price": 19.95,
      "starRating": 3.2,
      "imageUrl": "assets/images/leaf_rake.png"
    },
    {
      "productId": 2,
      "productName": "Dog Collars -Adventurous",
      "productCode": "GDN-0023",
      "releaseDate": "March 18, 2021",
      "description": "Safety Reflective Collar: Keep your furry friend safe during nighttime walks with our reflective dog collar. Designed with a special reflective strip, it ensures visibility in low-light conditions, enhancing your dog's safety.",
      "price": 32.99,
      "starRating": 4.2,
      "imageUrl": "assets/images/garden_cart.png"
    },
    {
      "productId": 3,
      "productName": "Dog Collars -Flexible",
      "productCode": "TBX-0048",
      "releaseDate": "May 21, 2021",
      "description": "Our adjustable nylon dog collar is perfect for dogs of all sizes. Made from durable, weather-resistant nylon, it's built to withstand the elements. With its easy-to-use buckle, finding the right fit for your pet is a breeze",
      "price": 8.9,
      "starRating": 4.8,
      "imageUrl": "assets/images/hammer.png"
    }]
  }

  onBack(): void {
    this.router.navigate(['/products']);
  }

  submit(){
    console.log(this.form.value);
  }
}
