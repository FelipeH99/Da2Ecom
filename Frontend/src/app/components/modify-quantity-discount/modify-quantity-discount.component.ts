import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { throwError } from 'rxjs';
import { ProductCategory } from 'src/app/classes/ProductCategory';
import { QuantityDiscount } from '../../classes/QuantityDiscount';
import { quantityDiscountService } from './../../services/quantity-discount.service';
import { ApiMessage } from 'src/app/classes/ApiMessage';
import { ColorDiscount } from 'src/app/classes/ColorDiscount';

@Component({
  selector: 'app-modify-quantity-discount',
  templateUrl: './modify-quantity-discount.component.html',
  styleUrls: ['./modify-quantity-discount.component.sass'],
  providers: [quantityDiscountService],
})
export class ModifyQuantityDiscountComponent implements OnInit {
  discounts: Array<QuantityDiscount>;
  productCategory: ProductCategory;
  selectedDiscount: QuantityDiscount;

  minProductsForPromotion: number;
  numberOfProductsToBeFree: number;
  productToBeDiscounted: string;
  isActive: boolean;
  name: string;
  myCategories = Object.keys(ProductCategory);

  constructor(private _quantityDiscountService: quantityDiscountService) {}

  ngOnInit(): void {
    this._quantityDiscountService
      .getAllDiscounts()
      .subscribe((discounts) => (this.discounts = discounts));
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
      var newDiscount = new QuantityDiscount();
      newDiscount.name = this.name;
      newDiscount.category = this.productCategory;
      newDiscount.productToBeDiscounted = this.productToBeDiscounted;
      newDiscount.minProductNeededForDiscount = this.minProductsForPromotion;
      newDiscount.numberOfProductToBeFree = this.numberOfProductsToBeFree;
      newDiscount.isActive = this.isActive;
      this._quantityDiscountService
        .modifyDiscount(this.selectedDiscount.id, newDiscount)
        .subscribe((response) => this.handleOutput(response));
    }
  }
  validateFields() {
    if (
      this.name == undefined ||
      this.minProductsForPromotion == undefined ||
      this.productToBeDiscounted == undefined ||
      this.numberOfProductsToBeFree == undefined ||
      this.isActive == undefined ||
      this.productCategory == undefined
    ) {
      alert('All fields must be completed to proceed');
      return false;
    }
    if (
      this.minProductsForPromotion <= 0 ||
      this.numberOfProductsToBeFree <= 0
    ) {
      alert('All numeric fields must have a value bigger than 0');
      return false;
    }
    return true;
  }
}
