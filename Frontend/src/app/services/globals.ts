import { Injectable } from '@angular/core';
import { count } from 'rxjs';

@Injectable()
export class Globals {
  constructor() {
  }
  private loggedUserId:string;

  isLogged(): Boolean {
    return localStorage.getItem('Token') != null;
  }

  getToken() {
    return localStorage.getItem('Token');
  }
  setLoggedUser(guid:string){
    this.loggedUserId = guid;
  }
  getLoggedUser(){
    console.log(this.loggedUserId);
    return this.loggedUserId;
  }
  hasPermission(request:string){
    var result = localStorage.getItem("permissions")?.search(request);
    return  result != -1;
  }
}
