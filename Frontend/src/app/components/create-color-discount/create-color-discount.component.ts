import { Component } from '@angular/core';
import { colorDiscountService } from 'src/app/services/color-discount.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ColorDiscount } from 'src/app/classes/ColorDiscount';
import { Color } from 'src/app/classes/Colors';
import { ColorService } from 'src/app/services/color.service';

@Component({
  selector: 'app-create-color-discount',
  templateUrl: './create-color-discount.component.html',
  styleUrls: ['./create-color-discount.component.sass'],
  providers: [colorDiscountService, ColorService],
})
export class CreateColorDiscountComponent {
  name: string;
  colorId: string;
  percentageDiscounted: number;
  productToBeDiscounted: string;
  minProductsNeededForDiscount: number;

  myColors: Array<Color>;

  constructor(
    private _colorDiscountService: colorDiscountService,
    private _colorService: ColorService
  ) {}
  ngOnInit() {
    this._colorService
      .getColor()
      .subscribe((colors) => (this.myColors = colors));
  }

  createSucceded() {
    alert('El descuento se ha creado correctamente');
    window.location.reload();
  }

  createFailed(error: HttpErrorResponse) {
    alert(error);
  }

  createColorDiscountObject() {
    let colorDiscount = new ColorDiscount();
    colorDiscount.name = this.name;
    colorDiscount.colorId = this.colorId;
    colorDiscount.percentageDiscount = this.percentageDiscounted;
    colorDiscount.productToBeDiscounted = this.productToBeDiscounted;
    colorDiscount.minProductsNeededForDiscount =
      this.minProductsNeededForDiscount;
    return colorDiscount;
  }

  onFormSubmit() {
    if (this.validateFields()) {
      var newDiscount = this.createColorDiscountObject();
      this._colorDiscountService.createDiscount(newDiscount).subscribe(
        (discount) => this.createSucceded(),
        (error) => this.createFailed(error)
      );
    }
  }
  validateFields() {
    if (
      this.colorId == undefined ||
      this.name == undefined ||
      this.minProductsNeededForDiscount == undefined ||
      this.productToBeDiscounted == undefined ||
      this.percentageDiscounted == undefined
    ) {
      alert('All fields must be completed to proceed');
      return false;
    }
    if (
      this.percentageDiscounted <= 0 ||
      this.minProductsNeededForDiscount <= 0
    ) {
      alert('You must specify a number bigger than 0 for numeric fields');
      return false;
    }
    return true;
  }
}
