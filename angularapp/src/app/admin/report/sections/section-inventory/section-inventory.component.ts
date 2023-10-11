import { Component, OnInit } from '@angular/core';
import { SalesDataService } from '../../services/sales-data.service';
import { ProductVariant} from '../../../../shared/product-category';

@Component({
  selector: 'app-section-inventory',
  templateUrl: './section-inventory.component.html',
  styleUrls: ['./section-inventory.component.css']
})
export class SectionInventoryComponent implements OnInit {

  constructor(private _salesData: SalesDataService) { }

  productVariants?: ProductVariant[];
  total = 0;
  page = 1;
  limit = 10;
  loading = false;
  title = "products";

  ngOnInit() {
    this.getInventory();
  }

  getInventory(): void {
    this._salesData.getProducts(this.page, this.limit)
      .subscribe(
        (res) => {
          this.productVariants = res.Page.Data;
          this.total = res.Page.Total;
          this.loading = false;
        }
      )
  }

  goToPrevious(): void {
    this.page--;
    this.getInventory();
  }

  goToNext(): void {
    this.page++;
    this.getInventory();
  }

  goToPage(n: number): void {
    this.page = n;
    this.getInventory();
  }
}
