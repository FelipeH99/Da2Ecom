import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Globals } from '../globals';

@Injectable()
export class LoggedGuard implements CanActivate {

    constructor(private _router: Router, private _globals: Globals) { }

    canActivate(route: ActivatedRouteSnapshot): boolean {
        if (this._globals.isLogged()) {
            this._router.navigateByUrl('/home');
            alert("Ya se encuentra loggeado.");
            return false;
        }
        return true;
    }
}
