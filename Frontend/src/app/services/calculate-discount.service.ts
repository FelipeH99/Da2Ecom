import { Observable, catchError, map, throwError } from "rxjs";
import { DiscountApplied } from "../classes/DiscountApplied";
import { Product } from "../classes/Product";
import { environment } from "../environments/environment";
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from "@angular/core";
@Injectable()
export class CalculateDiscountService{
  constructor(private _httpService: HttpClient){}

  get(): Observable<DiscountApplied>{
    let params = this.createParams();
    const url = `${environment.apiUrl}/discounts`;
    return this._httpService.get<DiscountApplied>(url,{params:params}).pipe(
      map((response) => {
        return response;
      }),catchError(this.handleError)
    )
  }
  createParams(){
    var products = localStorage.getItem("cart");
    let params = new HttpParams();
    params = params.set("productsIds",this.validateProductsIdsList(products));
    return params;
  }
  validateProductsIdsList(products:any){
    if(products){
      return products;
    }else{
      return null;
    }
  }
  private handleError(error: HttpErrorResponse) {
    let message: string;
    console.log("ERROR: " + JSON.stringify(error));
    if (error.error instanceof ErrorEvent) {
      message = "Error: do it again";
    } else {
      if (error.status == 0) {
        message = "The server is shutdown";
      } else {
        message = error.error;
      }
    }
    return throwError((message:any) =>
      new Error(message))
    ;
  }
}
