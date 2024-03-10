import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { throwError } from 'rxjs';
import { PercentageDiscount } from '../../classes/PercentageDiscount';
import { percentageDiscountService } from './../../services/percentage-discount.service';
import { ApiMessage } from 'src/app/classes/ApiMessage';
import { ColorDiscount } from 'src/app/classes/ColorDiscount';

@Component({
  selector: 'app-modify-percentage-discount',
  templateUrl: './modify-percentage-discount.component.html',
  styleUrls: ['./modify-percentage-discount.component.sass'],
  providers: [percentageDiscountService],
})
export class ModifyPercentageDiscountComponent implements OnInit {
  discounts: Array<PercentageDiscount>;
  selectedDiscount: PercentageDiscount;

  name: string;
  minProductsForPromotion: number;
  percentageDiscounted: number;
  productToBeDiscounted: string;
  isActive: boolean;

  constructor(private _percentageDiscountService: percentageDiscountService) {}

  ngOnInit(): void {
    this._percentageDiscountService.getAllDiscounts().subscribe(
      (response) => {
        this.discounts = response;
      },
      (error: HttpErrorResponse) => {
        console.error(error);
        return throwError(error);
      }
    );
  }
  showForm(id: string) {
    this.selectedDiscount = this.discounts.find((element) => element.id == id)!;
  }
  handleOutput(message: ApiMessage) {
    alert(message);
    window.location.reload();
  }

  onFormSubmit() {
    if (this.validateFields()) {
      var newDiscount = new PercentageDiscount();
      newDiscount.name = this.name;
      newDiscount.percentageDiscounted = this.percentageDiscounted;
      newDiscount.productToBeDiscounted = this.productToBeDiscounted;
      newDiscount.minProductNeededForDiscount = this.minProductsForPromotion;
      newDiscount.isActive = this.isActive;
      this._percentageDiscountService
        .modifyDiscount(this.selectedDiscount.id, newDiscount)
        .subscribe((response) => this.handleOutput(response));
    }
  }
  validateFields() {
    if (
      this.name == undefined ||
      this.minProductsForPromotion == undefined ||
      this.productToBeDiscounted == undefined ||
      this.percentageDiscounted == undefined ||
      this.isActive == undefined
    ) {
      alert('All fields must be completed to proceed');
      return false;
    }
    if (this.minProductsForPromotion <= 0 || this.percentageDiscounted <= 0) {
      alert('All numeric fields must have a value bigger than 0');
      return false;
    }
    return true;
  }
}
