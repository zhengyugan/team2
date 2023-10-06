import { Component, SimpleChange, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from './product';
import { initial } from 'lodash';

@Component({
  selector: 'pm-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {
  pageTitle: string = "Product Detail";
  product: IProduct | undefined;
  promotedProduct : IProduct[] | undefined;
  colourList: string[] = [];
  sizeList:string[] = [];
  hasError:boolean = false;
  errorMessage:string ="";

  form = new FormGroup({  
    colour: new FormControl('', Validators.required),
    size: new FormControl('', Validators.required),
    quantity: new FormControl('', Validators.required) 
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
    this.colourList =["red","blue","orange"];
    this.sizeList = ["l","m","n"];
    this.form.controls['quantity'].setValue("1");
  }

 

  updateQuantity(operation :string): void {
     var initialQuantity:number = Number(this.form.controls['quantity'].value); 
    if(operation == "plus" ){
       initialQuantity++
       this.form.controls['quantity'].setValue(initialQuantity.toString());
    }else{
      initialQuantity--
      this.form.controls['quantity'].setValue(initialQuantity.toString());
    }
  }

  onSubmit(){
    if(this.form.valid){
      console.log(this.form.value);
    }else{
      this.validation();
    }
    // this.router.navigate(['/products']);
   
    // use async await to insert item to cart
    // if buy now is click, navigate to cart page directly
    // 
  }

  validation(){
    this.hasError=false;
    this.errorMessage="";
    if(this.form.controls['colour'].invalid){
      this.errorMessage="Please select colour variation";
      this.hasError=true;
    }
    if(this.form.controls['size'].invalid){
      this.errorMessage="Please select size variation";
      this.hasError=true;
    }
  }

  getElementId(event:any){
  
    // Get the source element
    let element = event.target || event.srcElement || event.currentTarget;
    // Get the id of the source element
    let elementId = element.id;
    alert("The button's id is: " + elementId);  // Prompt element's id
  }
}
