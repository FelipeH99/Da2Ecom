import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, tap, throwError } from 'rxjs';
import { environment } from '../environments/environment';
import { Roles } from '../classes/Role';

@Injectable()
export class RoleService {
  constructor(private _httpService: HttpClient) {}
  getRoles() {
    const url = `${environment.apiUrl}/roles`;
    const HttpOptions = this.getHeaders();
    return this._httpService.get<Array<Roles>>(url, HttpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }
  getRolePermissions(userId: string) {
    const url = `${environment.apiUrl}/roles/${userId}`;
    const HttpOptions = this.getHeaders();
    return this._httpService
      .get<Array<string>>(url, HttpOptions)
      .pipe(tap((permissions) => this.finishRequest(permissions)));
  }
  finishRequest(permissions: Array<string>) {
    var permissionsString = '';
    permissions.forEach(function (item) {
      var newString = item + '*';
      permissionsString += newString;
    });
    localStorage.setItem('permissions', permissionsString);
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
