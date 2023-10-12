import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from '../product';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ProductService } from '../product.service';

@Component({
  templateUrl: '../product-detail/product-detail.component.html',
  styleUrls: ['../product-detail/product-detail.component.css']
})
export class AdminProductDetailComponent implements OnInit {
  pageTitle: string = "Product Detail";
  errorMessage: string = "";
  hideVariantDiv = true;
  hideLengthDiv = true;
  hideSizeDiv = true;

  product: any;

  updateForm: FormGroup;

  constructor(
    private route: ActivatedRoute, 
    private router: Router, 
    private formBuilder: FormBuilder,
    private productService: ProductService
    ) {

    this.updateForm = this.formBuilder.group({
      id: '',
      name: '',
      description: '',
      price: '',
      productCategoryId: '',
      sizingType: '',
      url: '',
      productVariants: this.formBuilder.array([]),
    })
  }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get("id"));

    this.productService.showProduct(id).subscribe((response) => {
        
        this.product = response;
        this.product = this.product.Data[0];

        this.updateForm.controls['id'].setValue(this.product.id);
        this.updateForm.controls['name'].setValue(this.product.name);
        this.updateForm.controls['description'].setValue(this.product.desc);
        this.updateForm.controls['price'].setValue(this.product.product_variants[0].price);
        this.updateForm.controls['productCategoryId'].setValue(this.product.product_category.id);
        this.updateForm.controls['sizingType'].setValue(this.product.sizing_type);

        this.onItemChange(this.product.sizing_type);

        var product_variants: any = this.product.product_variants; 
        var i:number = 0; 

        for(i = 0; i < product_variants.length ; i++) 
        {
          this.productVariants().push(this.formBuilder.group({
            productVariantId: product_variants[i].id,
            color: product_variants[i].color,
            length: product_variants[i].length,
            size: product_variants[i].size,
            quantity: product_variants[i].quantity,
          }));
        }
    });
  }

  onBack(): void {
    this.router.navigate(['/admin/product']);
  }

  onItemChange(sizingType: any){

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
    return (this.updateForm.get("productVariants") as FormArray) 
  };

  newProductVariants(): FormGroup {
    return this.formBuilder.group({
      id: '',
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
    this.productService.patchProducts( this.updateForm.value);
    var r = confirm("saved success");
    if(r)
    {
      this.router.navigate(['/admin/product']);
    }
  }
}