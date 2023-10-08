import { Component} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from '../../shared/product';

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
  isButtonDisabled = false;

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

    if(operation == "minus" && initialQuantity>0){
       initialQuantity--;
       this.form.controls['quantity'].setValue(initialQuantity.toString());
       if(initialQuantity == 0){
        this.isButtonDisabled = true;
       }
    }else{
      initialQuantity++;
      this.isButtonDisabled = false;
      this.form.controls['quantity'].setValue(initialQuantity.toString());
    }
  }

  onSubmit(buttonClick: string){
    this.validation();
    if(!this.hasError){
      // insert the data
      console.log(this.form.value);
    }
    
    if(buttonClick == "buyNow")
    {
      this.router.navigate(['/cart']);
    }
    
  }


  validation(){
    this.hasError=false;
    this.errorMessage="";
    // call backend api to insert the table
    // do all the handling and remove the message
    if(this.form.controls['colour'].invalid){
      this.errorMessage="Please select colour variation";
      this.hasError=true;
    }
    if(this.form.controls['size'].invalid){
      this.errorMessage="Please select size variation";
      this.hasError=true;
    }
  }

}
