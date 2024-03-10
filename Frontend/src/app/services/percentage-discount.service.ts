import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ApiMessage } from '../classes/ApiMessage';
import { environment } from '../environments/environment';
import { PercentageDiscount } from './../classes/PercentageDiscount';

@Injectable()
export class percentageDiscountService {
  constructor(private _httpService: HttpClient) {}

  createDiscount(
    percentageDiscount: PercentageDiscount
  ): Observable<PercentageDiscount> {
    const url = `${environment.apiUrl}/discounts/percentages`;
    const httpOptions = this.getHeaders();
    return this._httpService
      .post<PercentageDiscount>(url, percentageDiscount, httpOptions)
      .pipe(
        map((response) => {
          return response;
        }),
        catchError(this.handleError)
      );
  }
  modifyDiscount(
    id: string,
    percentageDiscount: PercentageDiscount
  ): Observable<ApiMessage> {
    const url = `${environment.apiUrl}/discounts/percentages/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService
      .put<ApiMessage>(url, percentageDiscount, httpOptions)
      .pipe(
        map((response) => {
          return response;
        }),
        catchError(this.handleError)
      );
  }

  deleteDiscount(id: string): Observable<ApiMessage> {
    const url = `${environment.apiUrl}/discounts/percentages/${id}`;
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
  getAllDiscounts(): Observable<PercentageDiscount[]> {
    const url = `${environment.apiUrl}/discounts/percentages`;
    const httpOptions = this.getHeaders();
    return this._httpService.get<PercentageDiscount[]>(url, httpOptions).pipe(
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
