import { environment } from '../environments/environment';
import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { Purchase } from '../classes/Purchase';

@Injectable()
export class purchaseService {
  constructor(private _httpService: HttpClient) {}

  getPurchases(): Observable<Array<Purchase>> {
    const url = `${environment.apiUrl}/purchases`;
    const httpHeaders = this.getHeaders();
    return this._httpService
      .get<Array<Purchase>>(url, httpHeaders)
      .pipe(
        map((response) => {
          return response;
        }),
        catchError(this.handleError)
      );
  }
  getId(id: string): Observable<Array<Purchase>> {
    const url = `${environment.apiUrl}/purchases/${id}`;
    const httpHeaders = this.getHeaders();
    return this._httpService
      .get<Array<Purchase>>(url, httpHeaders)
      .pipe(
        map((response) => {
          return response;
        }),
        catchError(this.handleError)
      );
  }
  createPurchase(purchaseModel: Purchase): Observable<Purchase> {
    const url = `${environment.apiUrl}/purchases`;
    const httpHeaders = this.getHeaders();
    return this._httpService
      .post<Purchase>(url, purchaseModel, httpHeaders)
      .pipe(
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
    const token = localStorage.getItem('Token') ?? '';
    const headers = new HttpHeaders({
      'accept': 'application/json',
      'Content-Type': 'application/json',
      'Auth': token,
    });
    return {headers};
  }
}
