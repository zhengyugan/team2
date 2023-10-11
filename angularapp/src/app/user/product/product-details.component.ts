import { Component} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Products } from '../../shared/product';
import { ProductService } from './product.service';
import { Subscription } from 'rxjs';
import { Carts } from 'src/app/shared/cart';
import { Variant } from 'src/app/shared/variant';

@Component({
  selector: 'pm-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {
  userId:number =1;
  sub!: Subscription;
  product: Products | undefined;
  productId: any;
  variantId: any;
  colourList: string[] = [];
  typeList:string[] = [];
  variantList:Variant[] =[];
  hasError:boolean = false;
  errorMessage:string ="";
  isButtonDisabled = false;
  showMyMessage = false;

  form = new FormGroup({  
    colour: new FormControl('', Validators.required),
    type: new FormControl('', Validators.required),
    quantity: new FormControl('', Validators.required) 
  });  

  constructor(private route: ActivatedRoute, private router: Router,private productService: ProductService) { }

  ngOnInit(): void {
    this.productId = Number(this.route.snapshot.paramMap.get("id"));
    this.getProductById();
    this.getProductVariant();
    this.form.controls['quantity'].setValue("1");
  }

  getProductById(){
    this.sub = this.productService.getProductbyId(this.productId).subscribe({
      next: product => {
        this.product = product['Data'][0];
      },
    });
  }

  getProductVariant(){
    var _colourList:string[]=[];
    var _typeList:string[]=[];
    this.sub = this.productService.getProductVariantById(this.productId).subscribe({
      next: variant => {
        this.variantList=variant['Data'];
        this.variantList.forEach(function (variant) {
          if(!_colourList.includes(variant.color) && variant.quantity>0){
            _colourList.push(variant.color);
          }

       
          if(variant.size!=null){
            if(!_typeList.includes(variant.size) && variant.quantity>0){
              _typeList.push(variant.size);
              }
          }else{
            if(!_typeList.includes(variant.length) && variant.quantity>0){
              _typeList.push(variant.length);
             }
          }
        }); 

        this.colourList = _colourList.sort();
        this.typeList = _typeList.sort();
      },
    });
    
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
      var cartInfo:Carts = this.cartDataMapping();
      this.productService.addItemToCart(cartInfo)
    }
    
    if(buttonClick == "buyNow")
    {
      this.router.navigate(['/cart']);
    }else{
      this.showMessage();
    }
    
  }

  cartDataMapping(){
    var carts:Carts = {
      "user_id" :this.userId,
      "product_variant_id":this.variantId,
      "quantity":Number(this.form.controls['quantity'].value)
    }
    return carts;
  }

  mapProductVariantId(type:any,color:any){
    var productVariantId =-1;
    this.variantList.forEach(function (variant){
      if((variant.size == type || variant.length==type) && variant.color == color){
        productVariantId = variant.id;
      }
    })
    return productVariantId;
  }

  validation(){
    this.hasError=false;
    this.errorMessage="";
    
    if(this.form.controls['colour'].invalid){
      this.errorMessage="Please select colour variation";
      this.hasError=true;
    }

    if(this.form.controls['type'].invalid){
      this.errorMessage="Please select size variation";
      this.hasError=true;
    }

    this.variantId = this.mapProductVariantId(this.form.controls['type'].value,this.form.controls['colour'].value);

    if(this.variantId ==-1 ){
      this.errorMessage="There is unexpexted error occurs. Please try again later";
      this.hasError=true;
    }
  }

  showMessage(){
    this.showMyMessage = true
    setTimeout(() => {
      this.showMyMessage = false
    }, 2000)
  }

}
