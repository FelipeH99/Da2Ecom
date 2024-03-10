import { Component, OnInit } from '@angular/core';
import { ApiMessage } from 'src/app/classes/ApiMessage';
import { BrandDiscount } from 'src/app/classes/BrandDiscount';
import { ColorDiscount } from 'src/app/classes/ColorDiscount';
import { PercentageDiscount } from 'src/app/classes/PercentageDiscount';
import { QuantityDiscount } from 'src/app/classes/QuantityDiscount';
import { brandDiscountService } from 'src/app/services/brand-discount.service';
import { colorDiscountService } from 'src/app/services/color-discount.service';
import { percentageDiscountService } from 'src/app/services/percentage-discount.service';
import { quantityDiscountService } from 'src/app/services/quantity-discount.service';

@Component({
  selector: 'app-delete-discount',
  templateUrl: './delete-discount.component.html',
  styleUrls: ['./delete-discount.component.sass'],
  providers: [
    brandDiscountService,
    quantityDiscountService,
    colorDiscountService,
    percentageDiscountService,
  ],
})
export class DeleteDiscountComponent {
  selectedType: string;
  errorMessage: string;
  display: boolean;

  myDiscounts: Array<any>;

  constructor(
    private _brandDiscountService: brandDiscountService,
    private _quantityDiscountService: quantityDiscountService,
    private _percentageDiscountService: percentageDiscountService,
    private _colorDiscountService: colorDiscountService
  ) {}
  ngOnInit() {
    this.display = false;
  }
  delete(id:string) {
    if (this.selectedType == 'Brand') {
      this._brandDiscountService.deleteDiscount(id).subscribe(
        (message) => this.deleteSucceded(message),
        (error) => this.deleteFailed(error)
      );
    } else if (this.selectedType == 'Quantity') {
      this._quantityDiscountService.deleteDiscount(id).subscribe(
        (message) => this.deleteSucceded(message),
        (error) => this.deleteFailed(error)
      );
    } else if (this.selectedType == 'Percentage') {
      this._percentageDiscountService.deleteDiscount(id).subscribe(
        (message) => this.deleteSucceded(message),
        (error) => this.deleteFailed(error)
      );
    } else if (this.selectedType == 'Color') {
      this._colorDiscountService.deleteDiscount(id).subscribe(
        (discount) => this.deleteSucceded(discount),
        (error) => this.deleteFailed(error)
      );
    } else {
      alert('Invalid type of discount, the operation was cancelled.');
    }
    this.display=false;
  }

  deleteSucceded(message: ApiMessage) {
    alert(message);
    window.location.reload();
  }

  deleteFailed(error: string) {
    this.errorMessage = error;
  }

  showDiscounts() {
    if (this.selectedType == 'Brand') {
      this._brandDiscountService.getAllDiscounts().subscribe((discount) => {
        this.myDiscounts = discount;
      });
    } else if (this.selectedType == 'Quantity') {
      this._quantityDiscountService.getAllDiscounts().subscribe((discount) => {
        this.myDiscounts = discount;
      });
    } else if (this.selectedType == 'Percentage') {
      this._percentageDiscountService
        .getAllDiscounts()
        .subscribe((discount) => {
          this.myDiscounts = discount;
        });
    } else if (this.selectedType == 'Color') {
      this._colorDiscountService.getAllDiscounts().subscribe((discount) => {
        this.myDiscounts = discount;
      });
    } else {
      alert('Invalid type of discount, the operation was cancelled.');
    }
    this.display = true;

  }
}
