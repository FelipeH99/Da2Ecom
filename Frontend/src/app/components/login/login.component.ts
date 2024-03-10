import { Component, OnInit, Output } from '@angular/core';
import { LoginService } from './../../services/login.service';
import { AdminToken } from './../../classes/AdminToken';
import { Router } from '@angular/router';
import { Login } from 'src/app/classes/Login';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/classes/User';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass'],
  providers: [UserService, LoginService, RoleService],
})
export class LoginComponent implements OnInit {
  email: string;
  password: string;
  errorMessage: string;
  newUserPassword: string;
  newUserPasswordRepeated: string;
  newUserEmail: string;
  newUserDeliveryAddress: string;
  newUserName: string;

  constructor(
    private _router: Router,
    private _loginService: LoginService,
    private _userService: UserService
  ) {}

  submitLogin() {
    const login = new Login(this.email, this.password);
    this._loginService.login(login).subscribe(
      (token: AdminToken) => this.logInSucceeded(token),
      (error) => this.handleError()
    );
  }
  submitSignIn() {
    const user = this.createNewUser();
    if (user) {
      this._userService.createUser(user).subscribe(
        (message) => this.createSucceded(),
        (error) => this.createFailed(error)
      );
    }
  }

  logInSucceeded(token: any) {
    this._router.navigateByUrl('home');
  }
  createSucceded() {
    alert('El usuario se ha creado correctamente.');
    this._router.navigateByUrl(`/home`);
  }
  createFailed(error: string) {
    alert(error);
  }
  handleError() {
    alert(
      'There was an error when login in, please verify your information and try again'
    );
  }

  createNewUser() {
    if (this.validateFields()) {
      let user = new User();
      user.name = this.newUserName;
      user.email = this.newUserEmail;
      user.password = this.newUserPassword;
      user.deliveryAdress = this.newUserDeliveryAddress;
      user.rolesId = [];
      return user;
    }
    return undefined;
  }
  validateFields() {
    if (
      this.newUserName == undefined ||
      this.newUserEmail == undefined ||
      this.newUserPassword == undefined ||
      this.newUserPasswordRepeated == undefined
    ) {
      alert('You must fill all the fields to continue');
      return false;
    }
    if (this.newUserPassword != this.newUserPasswordRepeated) {
      alert('Both passwords dont match!');
      return false;
    }
    return true;
  }

  ngOnInit() {}
}
