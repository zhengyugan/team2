import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from '../product';

@Component({
  templateUrl: '../product-detail/product-detail.component.html',
  styleUrls: ['../product-detail/product-detail.component.css']
})
export class AdminProductDetailComponent implements OnInit {
  pageTitle: string = "Product Detail";
  product: IProduct | undefined;

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get("id"));
    this.pageTitle += `: ${id}`;
    this.product = {
      "productId": id,
      "productName": "Leaf Rake",
      "productCategory": "Category B",
      "description": "Leaf rake with 48-inch wooden handle.",
      "price": 19.95,
      "starRating": 3.2,
      "imageUrl": "assets/images/leaf_rake.png"
    }
  }

  onBack(): void {
    this.router.navigate(['/admin/products']);
  }
}