import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ApiMessage } from 'src/app/classes/ApiMessage';
import { User } from 'src/app/classes/User';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-delete-users',
  templateUrl: './delete-users.component.html',
  styleUrls: ['./delete-users.component.sass'],
  providers: [UserService],
})
export class DeleteUsersComponent implements OnInit {
  userToModify: User;
  message: ApiMessage;
  users: Array<User>;
  constructor(
    private _userService: UserService,
    private _router: Router,
    private _loginService: LoginService
  ) {}
  ngOnInit(): void {
    this.message = new ApiMessage();
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
  }
  delete(id: string) {
    this._userService
      .deleteId(id)
      .subscribe((message) => (this.message = message));
  }
  close() {
    window.location.reload();
  }
}
