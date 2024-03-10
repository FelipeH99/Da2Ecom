import { AdminToken } from './../classes/AdminToken';
import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { environment } from '../environments/environment';
import { ApiMessage } from '../classes/ApiMessage';
import { Login } from '../classes/Login';
import { User } from '../classes/User';
import { RoleService } from './role.service';
import { Globals } from './globals';

@Injectable()
export class LoginService {
  constructor(
    private _httpService: HttpClient,
    private _roleService: RoleService,
    private _globals: Globals
  ) {}

  login(login: Login): Observable<AdminToken> {
    const url = `${environment.apiUrl}/sessions`;
    const httpOptions = this.getHeaders();
    return this._httpService
      .post<AdminToken>(url, login, httpOptions)
      .pipe(tap((session: AdminToken) => this.finishLogin(session)));
  }

  logout(id: string): Observable<ApiMessage> {
    const url = `${environment.apiUrl}/sessions/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService
      .delete<ApiMessage>(url, httpOptions)
      .pipe(tap(() => this.finishLogout()));
  }
  getUserFromSession(id: string): Observable<User> {
    const url = `${environment.apiUrl}/sessions/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService.get<User>(url, httpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }

  private finishLogin(session: AdminToken) {
    localStorage.setItem('Token', session.token);
    this.getUserFromSession(session.token).subscribe((user: User) => {
      this._globals.setLoggedUser(user.id);
      this._roleService.getRolePermissions(user.id).subscribe();
    });
  }
  private finishLogout() {
    localStorage.removeItem('Token');
    localStorage.removeItem('permissions');
    this._globals.setLoggedUser('');
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
}
