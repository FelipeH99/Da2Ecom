import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { throwError } from 'rxjs';
import { Color } from 'src/app/classes/Colors';
import { ColorDiscount } from '../../classes/ColorDiscount';
import { colorDiscountService } from './../../services/color-discount.service';
import { ColorService } from 'src/app/services/color.service';
import { ApiMessage } from 'src/app/classes/ApiMessage';

@Component({
  selector: 'app-modify-color-discount',
  templateUrl: './modify-color-discount.component.html',
  styleUrls: ['./modify-color-discount.component.sass'],
  providers: [colorDiscountService, ColorService],
})
export class ModifyColorDiscountComponent implements OnInit {
  discounts: Array<ColorDiscount>;
  selectedDiscount: ColorDiscount;
  colorId: string;
  minProductsForPromotion: number;
  percentageDiscounted: number;
  productToBeDiscounted: string;
  myColors: Array<Color>;
  name: string;

  isActive: boolean;

  constructor(
    private _colorDiscountService: colorDiscountService,
    private _colorService: ColorService
  ) {}

  ngOnInit() {
    this._colorDiscountService.getAllDiscounts().subscribe(
      (colorDiscounts) => {
        this.discounts = colorDiscounts;
      },
      (error: HttpErrorResponse) => {
        console.error(error);
        return throwError(error);
      }
    );
    this._colorService
      .getColor()
      .subscribe((colors) => (this.myColors = colors));
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
      var newDiscount = new ColorDiscount();
      newDiscount.name = this.name;
      newDiscount.percentageDiscount = this.percentageDiscounted;
      newDiscount.colorId = this.colorId;
      newDiscount.productToBeDiscounted = this.productToBeDiscounted;
      newDiscount.minProductsNeededForDiscount = this.minProductsForPromotion;
      newDiscount.isActive = this.isActive;
      this._colorDiscountService
        .modifyDiscount(this.selectedDiscount.id, newDiscount)
        .subscribe((response) => this.handleOutput(response));
    }
  }
  validateFields() {
    if (
      this.colorId == undefined ||
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
      alert('All numeric fields must have values bigger than 0');
      return false;
    }
    return true;
  }
}
