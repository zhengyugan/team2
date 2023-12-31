import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpEvent, HttpParams, HttpRequest } from "@angular/common/http";
import { Observable, catchError, map, tap, throwError } from "rxjs";

import { IProduct } from "./product";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private productUrl = 'api/products/products.json';
  private url ='https://team2webapp001.azurewebsites.net/';
  title = 'Product';
  
  constructor(private http: HttpClient) { }
  products:any =[];

  getProducts() {
    return this.http.get(this.url+'api/Product').pipe
    (
      tap( response =>JSON.stringify(response)),
      catchError(this.handleError)
    );
  }

  public getAllProducts(){

    return this.http.get(this.url+'api/Product/GetAllProducts').subscribe(data=>{
      this.products = JSON.stringify(data);
    })
  }

  public deleteProduct(id:number){
    const __param = new HttpParams({});
    return this.http.put(this.url + 'api/Product/DeleteItem/' + id, __param).pipe(map((res: any) => res || []));
  }

  upload(file: File): Observable<HttpEvent<any>> {
    const formData: FormData = new FormData();

    formData.append('file', file);

    const req = new HttpRequest('POST', `${this.url}/upload`, formData, {
      responseType: 'json',
    });

    return this.http.request(req);
  }

  getFiles(): Observable<any> {
    return this.http.get(`${this.url}/files`);
  }

  storeProducts(data: any){
    return this.http.post(this.url + 'api/Product/StoreProduct', data).pipe(map((res: any) => res || []));
  }

  patchProducts(data: any){
    return this.http.patch(this.url + 'api/Product/PatchProduct', data).subscribe(responsedata=>{
    });
  }

  showProduct(id: any)
  {
    return this.http.get(this.url+'api/Product/' + id).pipe(map(res => res || []));
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
