import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Brand } from '../classes/Brand';
import { environment } from '../environments/environment';

@Injectable()
export class BrandService {
  static getBrand(): Brand[] {
    throw new Error('Method not implemented.');
  }
  constructor(private _httpService: HttpClient) {}

  getBrand(): Observable<Array<Brand>> {
    const url = `${environment.apiUrl}/brands`;
    return this._httpService.get<Array<Brand>>(url).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }
  getId(id: string): Observable<Brand> {
    const url = `${environment.apiUrl}/brands/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService.get<Brand>(url, httpOptions).pipe(
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
    const myHeaders = new HttpHeaders({
      Accept: 'application/json',
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
    });
    const httpOptions = { headers: myHeaders };
    return httpOptions;
  }
}
