import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Observable, map, throwError } from 'rxjs';
import { environment } from '../environments/environment.prod';
import { User } from '../classes/User';
import { catchError } from 'rxjs/operators';
import { ApiMessage } from '../classes/ApiMessage';

@Injectable()
export class UserService {
  constructor(private _httpService: HttpClient) {}

  getUsers(): Observable<Array<User>> {
    const url = `${environment.apiUrl}/users`;
    const HttpOptions = this.getHeaders();
    return this._httpService.get<Array<User>>(url, HttpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }

  createUser(user: User): Observable<User> {
    const url = `${environment.apiUrl}/users`;
    const httpOptions = this.getHeaders();
    return this._httpService.post<User>(url, user, httpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }

  modifyUser(id: string, user: User): Observable<ApiMessage> {
    const url = `${environment.apiUrl}/users/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService.put<ApiMessage>(url, user, httpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }

  deleteId(id: string): Observable<ApiMessage> {
    const url = `${environment.apiUrl}/users/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService.delete<ApiMessage>(url, httpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }

  getId(id: string): Observable<User> {
    const url = `${environment.apiUrl}/users/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService.get<User>(url, httpOptions).pipe(
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
      Auth: token ? token : '',
    });
    const httpOptions = { headers: myHeaders };
    return httpOptions;
  }
}
