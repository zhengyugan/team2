import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()
export class SalesDataService {
  baseUrl: string = 'https://localhost:7056/api/order/';

  constructor(private http: HttpClient) { }

  getOrders(pageIndex: number, pageSize: number): Observable<any> {
    return this.http.get(this.baseUrl + pageIndex + '/' + pageSize).pipe(map(res => res || []));
  }

  getOrdersByDate(startDate: string, endDate: string): Observable<any> {
    return this.http.get(this.baseUrl + 'bydate/' + startDate + '/' + endDate).pipe(map(res => res || []));
  }

  getOrdersByCustomer(n: number) {
    return this.http.get(this.baseUrl + 'bycustomer/' + n).pipe(map(res => res || []));
  }

  getLatestOrder(): Observable<any> {
    return this.http.get(this.baseUrl + 'latest').pipe(map(res => res || []));
  }
}
