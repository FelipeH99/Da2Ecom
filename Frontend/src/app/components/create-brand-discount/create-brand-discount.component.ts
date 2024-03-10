import { Component, OnInit } from '@angular/core';
import { brandDiscountService } from 'src/app/services/brand-discount.service';
import { Brand } from 'src/app/classes/Brand';
import { BrandDiscount } from 'src/app/classes/BrandDiscount';
import { HttpErrorResponse } from '@angular/common/http';
import { BrandService } from 'src/app/services/brand.service';

@Component({
  selector: 'app-create-brand-discount',
  templateUrl: './create-brand-discount.component.html',
  styleUrls: ['./create-brand-discount.component.sass'],
  providers: [brandDiscountService, BrandService],
})
export class CreateBrandDiscountComponent implements OnInit {
  name: string;
  brandId: string;
  minProductsForPromotion: number;
  numberOfProductsForFree: number;
  productToBeDiscounted: number;
  isActive: boolean;
  myBrands: Array<Brand>;

  constructor(
    private _brandDiscountService: brandDiscountService,
    private _brandService: BrandService
  ) {}
  ngOnInit() {
    this._brandService.getBrand().subscribe((brands) => {
      this.myBrands = brands;
    });
  }

  createSucceded() {
    alert('El descuento se ha creado correctamente');
    window.location.reload();
  }

  createFailed(error: HttpErrorResponse) {
    alert(error);
  }
  createBrandDiscountObject() {
    let brandDiscount = new BrandDiscount();
    brandDiscount.name = this.name;
    brandDiscount.brandId = this.brandId;
    brandDiscount.minProductsNeededForDiscount = this.minProductsForPromotion;
    brandDiscount.numberOfProductsForFree = this.numberOfProductsForFree;
    brandDiscount.productToBeDiscounted = this.productToBeDiscounted;
    brandDiscount.isActive = this.isActive;
    return brandDiscount;
  }

  onFormSubmit() {
    if (this.validateFields()) {
      var newDiscount = this.createBrandDiscountObject();
      this._brandDiscountService.createDiscount(newDiscount).subscribe(
        (discount) => this.createSucceded(),
        (error) => this.createFailed(error)
      );
    }
  }
  validateFields() {
    if (
      this.brandId == undefined ||
      this.name == undefined ||
      this.minProductsForPromotion == undefined ||
      this.productToBeDiscounted == undefined ||
      this.numberOfProductsForFree == undefined
    ) {
      alert('All fields must be completed to proceed');
      return false;
    }
    if (
      this.minProductsForPromotion <= 0 ||
      this.numberOfProductsForFree <= 0
    ) {
      alert('You must specify a number bigger than 0 for numeric fields');
      return false;
    }
    return true;
  }
}
