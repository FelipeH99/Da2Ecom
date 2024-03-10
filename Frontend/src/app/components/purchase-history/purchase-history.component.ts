import { Component, OnInit } from '@angular/core';
import { Purchase } from '../../classes/Purchase';
import { purchaseService } from '../../services/purchase.service';
import { LoginService } from '../../services/login.service';
import { User } from '../../classes/User';

@Component({
  selector: 'app-purchase-history',
  templateUrl: './purchase-history.component.html',
  styleUrls: ['./purchase-history.component.sass'],
  providers: [purchaseService, LoginService],
})
export class PurchaseHistoryComponent implements OnInit {
  purchases: Array<Purchase>;
  user: User;
  error: string;
  constructor(
    private _purchaseService: purchaseService,
    private _loginService: LoginService
  ) {
    this.purchases = [];
  }
  ngOnInit(): void {
    var sessionToken = localStorage.getItem('Token');
    if (!sessionToken) {
      console.error('No user logged in');
      this.error = 'Fatal error - you have no permission to be here';
    } else {
      this._loginService.getUserFromSession(sessionToken).subscribe((user) => {
        this.user = user;
        this._purchaseService.getId(this.user.id).subscribe(
          (purchases) => (this.purchases = purchases),
          (error) => this.handleError(error)
        );
      });
    }
  }
  handleError(error: any) {
    this.error = "Error: No se encontraron compras para el usuario asociado";
  }
}
