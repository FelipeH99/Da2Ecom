import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { throwError } from 'rxjs';
import { Brand } from 'src/app/classes/Brand';
import { BrandService } from 'src/app/services/brand.service';
import { BrandDiscount } from '../../classes/BrandDiscount';
import { brandDiscountService } from './../../services/brand-discount.service';
import { ApiMessage } from 'src/app/classes/ApiMessage';

@Component({
  selector: 'app-modify-brand-discount',
  templateUrl: './modify-brand-discount.component.html',
  styleUrls: ['./modify-brand-discount.component.sass'],
  providers: [brandDiscountService, Brand, BrandService],
})
export class ModifyBrandDiscountComponent implements OnInit {
  discounts: Array<BrandDiscount>;
  selectedDiscount: BrandDiscount;

  brandId: string;
  minProductsForPromotion: number;
  numberOfProductsForFree: number;
  productToBeDiscounted: number;
  myBrands: Array<Brand>;
  isActive: boolean;
  name: string;

  constructor(
    private _brandDiscountService: brandDiscountService,
    private _brandService: BrandService
  ) {}

  ngOnInit(): void {
    this._brandDiscountService.getAllDiscounts().subscribe(
      (response: BrandDiscount[]) => {
        this.discounts = response;
      },
      (error: HttpErrorResponse) => {
        console.error(error);
        return throwError(error);
      }
    );
    this._brandService.getBrand().subscribe((brands) => {
      this.myBrands = brands;
    });
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
      var newDiscount = new BrandDiscount();
      newDiscount.name = this.name;
      newDiscount.numberOfProductsForFree = this.numberOfProductsForFree;
      newDiscount.brandId = this.brandId;
      newDiscount.productToBeDiscounted = this.productToBeDiscounted;
      newDiscount.minProductsNeededForDiscount = this.minProductsForPromotion;
      newDiscount.isActive = this.isActive;
      this._brandDiscountService
        .modifyDiscount(this.selectedDiscount.id, newDiscount)
        .subscribe((response) => this.handleOutput(response));
    }
  }
  validateFields() {
    if (
      this.brandId == undefined ||
      this.name == undefined ||
      this.minProductsForPromotion == undefined ||
      this.productToBeDiscounted == undefined ||
      this.numberOfProductsForFree == undefined ||
      this.isActive == undefined
    ) {
      alert('All fields must be completed to proceed');
      return false;
    }
    if (
      this.numberOfProductsForFree <= 0 ||
      this.minProductsForPromotion <= 0
    ) {
      alert('All numeric fields must have values bigger than 0.');
      return false;
    }
    return true;
  }
}
