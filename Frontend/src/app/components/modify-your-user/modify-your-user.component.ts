import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ApiMessage } from 'src/app/classes/ApiMessage';
import { Roles } from 'src/app/classes/Role';
import { User } from 'src/app/classes/User';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-modify-your-user',
  templateUrl: './modify-your-user.component.html',
  styleUrls: ['./modify-your-user.component.sass'],
  providers: [UserService],
})
export class ModifyYourUserComponent implements OnInit {
  userToModify: User;
  message: ApiMessage;
  form: FormGroup;
  roles: Array<Roles>;
  createdUser: User;
  email: string;
  name: string;
  password: string;
  adress: string;
  errorMessage: string;
  constructor(
    private _loginService: LoginService,
    private _userService: UserService
  ) {}
  ngOnInit(): void {
    this.userToModify = new User();
    var token = localStorage.getItem('Token');
    if (token) {
      this._loginService
        .getUserFromSession(token)
        .subscribe((user) => (this.userToModify = user));
    }
  }

  onFormSubmit() {
    if (
      this.validateName() &&
      this.validateEmail() &&
      this.validateAdress() &&
      this.validatePassword()
    ) {
      var newUser = new User();
      newUser.deliveryAdress = this.adress;
      newUser.email = this.email;
      newUser.name = this.name;
      newUser.password = this.password;
      newUser.isDeleted = false;
      console.log(this.userToModify.rolesId);
      newUser.rolesId = this.userToModify.rolesId;
      this._userService
        .modifyUser(this.userToModify.id, newUser)
        .subscribe((response) => this.handleOutput(response));
    }
  }
  validateName() {
    if (this.name == undefined) {
      this.errorMessage = 'Nombre cant be empty';
      return false;
    }
    return true;
  }
  validateEmail() {
    if (this.email == undefined) {
      this.errorMessage = 'Email cant be empty';
      return false;
    }
    return true;
  }
  validatePassword() {
    if (this.password == undefined) {
      this.errorMessage = 'Password cant be empty';
      return false;
    }
    return true;
  }

  validateAdress() {
    if (this.adress == undefined) {
      this.errorMessage = 'Adress cant be empty';
      return false;
    }
    return true;
  }
  handleOutput(message: ApiMessage) {
    this.message = message;
  }
  close() {
    window.location.reload();
  }
}
