import { Injectable } from '@angular/core';
import { ICart } from "./cart";
import { Observable, catchError, map, tap, throwError } from "rxjs";
import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { constant } from 'lodash';
import { __param } from 'tslib';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartUrl = 'api/cart/cart.json';
  baseUrl: string = 'https://localhost:7056/';

  constructor(private http: HttpClient) { }

  getCart(): Observable<ICart[]> {
    return this.http.get<ICart[]>(this.cartUrl).pipe(
      tap(data => console.log('All', JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  public getCartItem(id: number) {
    return this.http.get(this.baseUrl + 'api/Cart/' + id).pipe(map((res: any) => res || []));
  }

  public delete(id: number) {
    const __param = new HttpParams({});
    return this.http.put(this.baseUrl + 'api/Cart/' + id, __param).pipe(map((res: any) => res || []));
  }

  private handleError(err: HttpErrorResponse) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }
}
