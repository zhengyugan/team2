import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../product.service';


@Component({
  selector: 'pm-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css']
})
export class AdminProductCreateComponent implements OnInit{
  pageTitle: string = "Product Create";
  hideVariantDiv = true;
  hideLengthDiv = true;
  hideSizeDiv = true;

  createForm: FormGroup;

  constructor(
    private route: ActivatedRoute, 
    private router: Router, 
    private formBuilder: FormBuilder,
    private productService: ProductService
    ) { 
    this.createForm = this.formBuilder.group({
      name: '',
      description: '',
      price: '',
      productCategoryId: '',
      sizingType: '',
      productVariants: this.formBuilder.array([]),
    })
  }

  ngOnInit(): void {
    
  }

  onBack(): void {
    this.router.navigate(['/admin/products']);
  }

  onItemChange(sizingType: any){

    if(this.productVariants().length == 0)
    {
      this.addQuantity();
    }

    if(sizingType == 'length')
    {
      this.hideLengthDiv = false;
      this.hideSizeDiv = true;

      this.hideVariantDiv = false;

    }
    else if(sizingType == 'size')
    {
      this.hideLengthDiv = true;
      this.hideSizeDiv = false;

      this.hideVariantDiv = false;
    }
    else
    {
      this.hideLengthDiv = true;
      this.hideSizeDiv = true;

      this.hideVariantDiv = true;
    }
  }

  productVariants(): FormArray{
    return (this.createForm.get("productVariants") as FormArray) 
  };

  newProductVariants(): FormGroup {
    return this.formBuilder.group({
      color: '',
      length: '',
      size: '',
      quantity: ''
    })
  }

  addQuantity() {
    this.productVariants().push(this.newProductVariants());
  }

  removeQuantity() {
    this.productVariants().removeAt((this.productVariants().length)-1);
  }

  onSubmit() {
    console.log('formmmmmmmas', this.createForm.value);
    console.log('juj', this.productService.storeProducts( this.createForm.value));
    console.log('done');
  }

}
