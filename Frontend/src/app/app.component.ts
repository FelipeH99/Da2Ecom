import { Component, OnInit } from '@angular/core';
import { Globals } from './services/globals';
import { Router } from '@angular/router';
import { LoginService } from './services/login.service';
import { RoleService } from './services/role.service';
import { ApiMessage } from './classes/ApiMessage';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
  providers: [LoginService, RoleService],
})
export class AppComponent implements OnInit {
  message: string;
  title: string;
  apiResponse: ApiMessage;

  constructor(
    private _globals: Globals,
    private _router: Router,
    private _loginService: LoginService
  ) {
    this.apiResponse = new ApiMessage();
  }

  ngOnInit() {}
  isLogged() {
    return this._globals.isLogged();
  }
  hasPermissions(request: string) {
    if (this.isLogged()) {
      return this._globals.hasPermission(request);
    } else {
      return false;
    }
  }
  logout() {
    let token = localStorage.getItem('Token');
    if (!token) {
      this.title = 'Error';
      this.message = 'Unsuccessful logout - not logged in';
      return;
    }else{
      this._loginService.logout(token).subscribe((text) => {
        this.apiResponse = text;
        this.title = 'Success';
        this.message = this.apiResponse.mensaje;
      });
      this.title = 'Success';
      this.message = this.apiResponse.mensaje;
    }
  }
  close() {
    window.location.reload();
  }
}
