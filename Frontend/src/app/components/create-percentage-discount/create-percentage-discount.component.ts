import { Component, OnInit } from '@angular/core';
import { percentageDiscountService } from 'src/app/services/percentage-discount.service';
import { PercentageDiscount } from 'src/app/classes/PercentageDiscount';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-percentage-discount',
  templateUrl: './create-percentage-discount.component.html',
  styleUrls: ['./create-percentage-discount.component.sass'],
  providers: [percentageDiscountService]
})
export class CreatePercentageDiscountComponent implements OnInit{
  name: string;
  percentageDiscounted: number;
  productToBeDiscounted: string;
  minProductsNeededForDiscount: number;

  constructor(
    private _percentageDiscountService: percentageDiscountService) {
  }
  ngOnInit() {
  }
  createSucceded() {
    alert('El descuento se ha creado correctamente');
    window.location.reload();
  }

  createFailed(error: HttpErrorResponse) {
    alert(error);
  }
  createPercentageDiscountObject() {
    let percentageDiscount = new PercentageDiscount();
    percentageDiscount.name = this.name;
    percentageDiscount.percentageDiscounted = this.percentageDiscounted;
    percentageDiscount.productToBeDiscounted = this.productToBeDiscounted;
    percentageDiscount.minProductNeededForDiscount = this.minProductsNeededForDiscount;
    return percentageDiscount;
  }

  onFormSubmit() {
    if(this.validateFields()){
      var newDiscount = this.createPercentageDiscountObject();
      this._percentageDiscountService.createDiscount(newDiscount).subscribe(
        (discount) => this.createSucceded(),
        (error) => this.createFailed(error)
      );
    }

  }
  validateFields() {
    if (
      this.name == undefined ||
      this.minProductsNeededForDiscount == undefined ||
      this.productToBeDiscounted == undefined ||
      this.percentageDiscounted == undefined
    ) {
      alert('All fields must be completed to proceed');
      return false;
    }
    if (
      this.minProductsNeededForDiscount <= 0 ||
      this.percentageDiscounted <= 0
    ) {
      alert('You must specify a number bigger than 0 for numeric fields');
      return false;
    }
    return true;
  }

}
