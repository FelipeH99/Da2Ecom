import { Component, OnInit } from '@angular/core';
import { ProductCategory } from 'src/app/classes/ProductCategory';
import { quantityDiscountService } from 'src/app/services/quantity-discount.service';
import { HttpErrorResponse } from '@angular/common/http';
import { QuantityDiscount } from 'src/app/classes/QuantityDiscount';

@Component({
  selector: 'app-create-quantity-discount',
  templateUrl: './create-quantity-discount.component.html',
  styleUrls: ['./create-quantity-discount.component.sass'],
  providers: [quantityDiscountService],
})
export class CreateQuantityDiscountComponent {
  name: string;
  category: ProductCategory;
  minProductsNeededForDiscount: number;
  numberOfProductsToBeFree: number;
  productToBeDiscounted: string;
  myCategories = Object.keys(ProductCategory);

  constructor(private _quantityDiscountService: quantityDiscountService) {}
  ngOnInit() {}
  createSucceded() {
    alert('El descuento se ha creado correctamente');
    window.location.reload();
  }

  createFailed(error: HttpErrorResponse) {
    alert(error);
  }
  createQuantityDiscountObject() {
    let quantityDiscount = new QuantityDiscount();
    quantityDiscount.name = this.name;
    quantityDiscount.category = this.category;
    quantityDiscount.productToBeDiscounted = this.productToBeDiscounted;
    quantityDiscount.minProductNeededForDiscount =
      this.minProductsNeededForDiscount;
    quantityDiscount.numberOfProductToBeFree = this.numberOfProductsToBeFree;
    return quantityDiscount;
  }

  onFormSubmit() {
    if (this.validateFields()) {
      var newDiscount = this.createQuantityDiscountObject();
      this._quantityDiscountService.createDiscount(newDiscount).subscribe(
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
      this.category == undefined ||
      this.numberOfProductsToBeFree == undefined
    ) {
      alert('All fields must be completed to proceed');
      return false;
    }
    if (
      this.minProductsNeededForDiscount <= 0 ||
      this.numberOfProductsToBeFree <= 0
    ) {
      alert('You must specify a number bigger than 0 for numeric fields');
      return false;
    }
    return true;
  }
}
