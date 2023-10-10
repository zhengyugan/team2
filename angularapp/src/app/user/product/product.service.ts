import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, tap, throwError } from "rxjs";
import { map } from 'rxjs/operators';
import { IProduct } from "../../shared/product";
import { Carts } from "src/app/shared/cart";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private productUrl = 'api/products/products.json';
  private url='https://localhost:7056';
  title = 'Product';
  
  constructor(private http: HttpClient) { }
  products:any =[];

  getProducts(): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.productUrl).pipe(
      tap(data => console.log('All', JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  public getAllProducts(){
    return this.http.get(this.url + '/api/Product/GetAllProducts' ).pipe(map((res: any) => res || []));
  }

  public getProductbyId(id:number){
    return this.http.get(this.url + '/api/Product/GetProductById/'+ id ).pipe(map((res: any) => res || []));
  }

  public getProductVariantById(id:number){
    return this.http.get(this.url + '/api/Product/GetProductVariation/'+ id ).pipe(map((res: any) => res || []));
  }

  public addItemToCart(data:Carts){
    return this.http.post(this.url + '/api/Product/AddItem',data ).pipe(map((res: any) => res || []));
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
