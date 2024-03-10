import { Component, OnInit } from '@angular/core';
import { ApiMessage } from 'src/app/classes/ApiMessage';
import { Roles } from 'src/app/classes/Role';
import { User } from 'src/app/classes/User';
import { LoginService } from 'src/app/services/login.service';
import { RoleService } from 'src/app/services/role.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-modify-user',
  templateUrl: './modify-user.component.html',
  styleUrls: ['./modify-user.component.sass'],
  providers: [UserService],
})
export class ModifyUserComponent implements OnInit {
  users: Array<User>;
  message: ApiMessage;
  userToModify: User;
  roles: Array<Roles>;
  name: string;
  adress: string;
  email: string;
  password: string;
  role: string;
  errorMessage: string;
  constructor(
    private _userService: UserService,
    private _roleService: RoleService,
    private _loginService: LoginService
  ) {
    this.users = [];
  }
  ngOnInit(): void {
    this._userService.getUsers().subscribe((users) => {
      var token = localStorage.getItem('Token');
      if (token) {
        this._loginService
          .getUserFromSession(token)
          .subscribe((currentUser) => {
            this.users = users.filter((user) => user.id != currentUser.id);
          });
      }
    });
    this._roleService.getRoles().subscribe((roles) => (this.roles = roles));
  }
  showForm(id: string) {
    this._userService.getId(id).subscribe((user) => (this.userToModify = user));
  }
  handleOutput(message: ApiMessage) {
    this.message = message;
  }
  close() {
    window.location.reload();
  }
  onFormSubmit() {
    if (
      this.validateName() &&
      this.validateEmail() &&
      this.validateAdress() &&
      this.validatePassword() &&
      this.validateRole()
    ) {
      var newUser = new User();
      newUser.deliveryAdress = this.adress;
      newUser.email = this.email;
      newUser.name = this.name;
      newUser.password = this.password;
      newUser.isDeleted = false;
      newUser.rolesId = [this.role];
      this._userService
        .modifyUser(this.userToModify.id, newUser)
        .subscribe((response) => this.handleOutput(response));
    }
  }
  validateName() {
    if (this.name == undefined) {
      this.errorMessage = 'Nombre no puede ser vacio';
      return false;
    }
    return true;
  }
  validateEmail() {
    if (this.email == undefined) {
      this.errorMessage = 'Email no puede ser vacio';
      return false;
    }
    return true;
  }
  validatePassword() {
    if (this.password == undefined) {
      this.errorMessage = 'Password no puede ser vacio';
      return false;
    }
    return true;
  }
  validateRole() {
    if (this.role == undefined) {
      this.errorMessage = 'Role no puede ser vacio';
      return false;
    }
    return true;
  }
  validateAdress() {
    if (this.adress == undefined) {
      this.errorMessage = 'Direccion no puede ser vacio';
      return false;
    }
    return true;
  }
}
