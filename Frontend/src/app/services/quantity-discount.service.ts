import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ApiMessage } from '../classes/ApiMessage';
import { QuantityDiscount } from '../classes/QuantityDiscount';
import { environment } from '../environments/environment';

@Injectable()
export class quantityDiscountService {
  constructor(private _httpService: HttpClient) {}

  createDiscount(
    quantityDiscount: QuantityDiscount
  ): Observable<QuantityDiscount> {
    const url = `${environment.apiUrl}/discounts/quantities`;
    const httpOptions = this.getHeaders();
    return this._httpService
      .post<QuantityDiscount>(url, quantityDiscount, httpOptions)
      .pipe(
        map((response) => {
          return response;
        }),
        catchError(this.handleError)
      );
  }
  modifyDiscount(
    id: string,
    quantityDiscount: QuantityDiscount
  ): Observable<ApiMessage> {
    console.log(quantityDiscount);
    const url = `${environment.apiUrl}/discounts/quantities/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService
      .put<ApiMessage>(url, quantityDiscount, httpOptions)
      .pipe(
        map((response) => {
          return response;
        }),
        catchError(this.handleError)
      );
  }

  deleteDiscount(id: string): Observable<ApiMessage> {
    const url = `${environment.apiUrl}/discounts/quantities/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService.delete<ApiMessage>(url, httpOptions).pipe(
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
  getAllDiscounts() {
    const url = `${environment.apiUrl}/discounts/quantities`;
    const httpOptions = this.getHeaders();
    return this._httpService.get<QuantityDiscount[]>(url, httpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }
  private getHeaders() {
    const token = localStorage.getItem('Token');
    const myHeaders = new HttpHeaders({
      Accept: 'application/json',
      'Content-Type': 'application/json',
      Auth: token ? token : '',
    });
    const httpOptions = { headers: myHeaders };
    return httpOptions;
  }
}
