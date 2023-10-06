import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IProduct } from '../../shared/product';
import { ProductService } from './product.service';


@Component({
  selector: 'pm-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, OnDestroy {
  errorMessage: string = "";
  sub!: Subscription;
  filteredProducts: IProduct[] = [];
  products: IProduct[] = [];
  selectedOption: string ="";
  private _listFilter: string = "";
  total = 0;
  page = 1;
  limit = 10;
  loading = false;

  get listFilter(): string {
    // set the size
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredProducts = this.performFilter(value);
  }

  constructor(private productService: ProductService) { }

  performFilter(filterBy: string): IProduct[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.products.filter((product: IProduct) =>
      product.productName.toLocaleLowerCase().includes(filterBy));
  }

  ngOnInit(): void {
   this.getProducts();
  }

  getProducts():void{
    this.sub = this.productService.getProducts().subscribe({
      next: products => {
        this.products = products;
        this.filteredProducts = this.products;
        this.total = this.products.length;
      },
    });
  }

  goToPrevious(): void {
    this.page--;
    this.getProducts();
  }

  goToNext(): void {
    this.page++;
    this.getProducts();
  }

  goToPage(n: number): void {
    this.page = n;
    this.getProducts();
  }

  onSelectionChange(){
    this.filteredProducts = this.performFilter(this.selectedOption);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
