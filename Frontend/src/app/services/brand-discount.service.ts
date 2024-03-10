import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ApiMessage } from '../classes/ApiMessage';
import { BrandDiscount } from '../classes/BrandDiscount';
import { environment } from '../environments/environment';

@Injectable()
export class brandDiscountService {
  constructor(private _httpService: HttpClient) {}

  createDiscount(brandDiscount: BrandDiscount): Observable<BrandDiscount> {
    const url = `${environment.apiUrl}/discounts/brands`;
    const httpOptions = this.getHeaders();
    return this._httpService
      .post<BrandDiscount>(url, brandDiscount, httpOptions)
      .pipe(
        map((response) => {
          return response;
        }),
        catchError(this.handleError)
      );
  }
  modifyDiscount(
    id: string,
    brandDiscount: BrandDiscount
  ): Observable<ApiMessage> {
    const url = `${environment.apiUrl}/discounts/brands/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService
      .put<ApiMessage>(url, brandDiscount, httpOptions)
      .pipe(
        map((response) => {
          return response;
        }),
        catchError(this.handleError)
      );
  }

  deleteDiscount(id: string): Observable<ApiMessage> {
    const url = `${environment.apiUrl}/discounts/brands/${id}`;
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
    const url = `${environment.apiUrl}/discounts/brands`;
    const httpOptions = this.getHeaders();
    return this._httpService.get<BrandDiscount[]>(url, httpOptions).pipe(
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
