import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IProduct, Products } from '../../shared/product';
import { ProductService } from './product.service';

@Component({
  selector: 'pm-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, OnDestroy {
  errorMessage: string = "";
  sub!: Subscription;
  filteredProducts: Products[] = [];
  products: Products[] = [];
  selectedOption: string ="";
  private _listFilter: string = "";
  total = 0;
  page = 1;
  limit = 8;
  loading = false;

  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredProducts = this.performFilter(value);
  }

  constructor(private productService: ProductService) { }

  performFilter(filterBy: string): Products[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.products.filter((product: Products) =>
      product.name.toLocaleLowerCase().includes(filterBy));
  }

  ngOnInit(): void {
   this.getAllProducts();
  }
  
  getAllProducts(){
    this.sub = this.productService.getAllProducts().subscribe({
      next: products => {
        this.products = products['Data'];
        this.filteredProducts = this.products;
        this.total = this.products.length;
      },
    });
  }

  goToPrevious(): void {
    this.page--;
    this.getAllProducts();
  }

  goToNext(): void {
    this.page++;
    this.getAllProducts();
  }

  goToPage(n: number): void {
    this.page = n;
    this.getAllProducts();
  }

  onSelectionChange(){
    this.filteredProducts = this.performFilter(this.selectedOption);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
