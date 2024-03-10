import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../environments/environment';
import { catchError, map, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { Color } from '../classes/Colors';
@Injectable()
export class ColorService {
  constructor(private _httpService: HttpClient) {}

  getColor(): Observable<Array<Color>> {
    const url = `${environment.apiUrl}/colors`;
    return this._httpService.get<Array<Color>>(url).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }
  getId(id: string): Observable<Color> {
    const url = `${environment.apiUrl}/colors/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService.get<Color>(url, httpOptions).pipe(
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
