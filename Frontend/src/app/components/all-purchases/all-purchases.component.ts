import { Component, OnInit } from '@angular/core';
import { Purchase } from 'src/app/classes/Purchase';
import { purchaseService } from 'src/app/services/purchase.service';

@Component({
  selector: 'app-all-purchases',
  templateUrl: './all-purchases.component.html',
  styleUrls: ['./all-purchases.component.sass'],
  providers: [purchaseService],
})
export class AllPurchasesComponent implements OnInit {
  purchases: Array<Purchase>;
  error: string;
  constructor(private _purchaseService: purchaseService) {
    this.purchases = [];
  }
  ngOnInit(): void {
    var sessionToken = localStorage.getItem('Token');
    if (sessionToken == null) {
      console.error('No user logged in');
      this.error = 'Fatal error - you have no permission to be here';
    } else {
      this._purchaseService.getPurchases().subscribe((purchases) => {
        this.purchases = purchases;
      });
    }
  }
}
