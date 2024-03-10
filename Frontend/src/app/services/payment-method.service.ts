import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map, catchError, throwError } from "rxjs";
import { ProductInformation } from "../classes/ProductInformation";
import { SearchResult } from "../classes/SearchResult";
import { environment } from "../environments/environment";
import { PaymentMethod } from "../classes/PaymentMethod";

@Injectable()
export class paymentMethodService{
  constructor(private _httpService: HttpClient){}
  getPayments(): Observable<Array<PaymentMethod>> {
    const url = `${environment.apiUrl}/payments`;
    const HttpOptions = this.getHeaders();
    return this._httpService.get<Array<any>>(url,HttpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }
  private handleError(error: HttpErrorResponse) {
    let message: string;
    console.log('ERROR: ' + JSON.stringify(error));
    if (error.error instanceof ErrorEvent) {
      message = 'Error: do it again';
    } else {
      if (error.status == 0) {
        message = 'The server is shutdown';
      } else {
        message = error.error;
      }
    }
    return throwError((message: any) => new Error(message));
  }

  private getHeaders() {
    const token = localStorage.getItem('Token');
    const myHeaders = new HttpHeaders({
      Accept: 'application/json',
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      Auth: token ? token : '',
    });
    const httpOptions = { headers: myHeaders };
    return httpOptions;
  }
}
