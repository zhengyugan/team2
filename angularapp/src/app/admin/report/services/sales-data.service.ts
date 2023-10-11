import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()
export class SalesDataService {
  baseUrl: string = 'https://localhost:7056/api/';

  constructor(private http: HttpClient) { }

  getProducts(pageIndex: number, pageSize: number): Observable<any> {
    return this.http.get(this.baseUrl + 'product/' + pageIndex + '/' + pageSize).pipe(map(res => res || []));
  }

  getOrders(pageIndex: number, pageSize: number): Observable<any> {
    return this.http.get(this.baseUrl + 'order/' + pageIndex + '/' + pageSize).pipe(map(res => res || []));
  }

  getOrdersByDate(startDate: string, endDate: string): Observable<any> {
    return this.http.get(this.baseUrl + 'order/bydate/' + startDate + '/' + endDate).pipe(map(res => res || []));
  }

  getOrdersByCustomer(n: number) {
    return this.http.get(this.baseUrl + 'order/bycustomer/' + n).pipe(map(res => res || []));
  }

  getLatestOrder(): Observable<any> {
    return this.http.get(this.baseUrl + 'order/latest').pipe(map(res => res || []));
  }
}
