import { Component, OnInit } from '@angular/core';
import { Order } from '../../../../shared/order';
import { SalesDataService } from '../../services/sales-data.service';

@Component({
  selector: 'app-section-orders',
  templateUrl: './section-orders.component.html',
  styleUrls: ['./section-orders.component.css']
})
export class SectionOrdersComponent implements OnInit {

  constructor(private _salesData: SalesDataService) { }

  orders?: Order[];
  total = 0;
  page = 1;
  limit = 10;
  loading = false;
  title = "orders";

  ngOnInit() {
    this.getOrders();
  }

  getOrders(): void {
    this._salesData.getOrders(this.page, this.limit)
      .subscribe(
        (res) => {
          console.log(res);
          this.orders = res.Page.Data;
          this.total = res.Page.Total;
          this.loading = false;
        }
    )
  }

  goToPrevious(): void {
    this.page--;
    this.getOrders();
  }

  goToNext(): void {
    this.page++;
    this.getOrders();
  }

  goToPage(n: number): void {
    this.page = n;
    this.getOrders();
  }
}
