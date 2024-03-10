import { productService } from './../../services/product.service';
import { Component, OnInit } from '@angular/core';
import { Brand } from 'src/app/classes/Brand';
import { Color } from 'src/app/classes/Colors';
import { BrandService } from 'src/app/services/brand.service';
import { ColorService } from 'src/app/services/color.service';
import { Product } from 'src/app/classes/Product';
import { Router } from '@angular/router';
import { ApiMessage } from 'src/app/classes/ApiMessage';
@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.sass'],
  providers: [BrandService, ColorService],
})
export class CreateProductComponent implements OnInit {
  product: Product;
  myBrands: Array<Brand>;
  myColors: Array<Color>;
  selectedColors: Array<Color>;
  actualSelectedColor: Color;
  error: string;
  name: string;
  description: string;
  price: number;
  brand: string;
  productCategory: string;
  url: string;
  message: string;
  stock: number;
  availableForPromotion: boolean;
  categories = [
    'Abrigos',
    'Cazadoras',
    'Chalecos',
    'Trajes',
    'Blazers',
    'Camisas',
    'Camisetas',
    'Polos',
    'Sudaderas',
    'Chandal',
    'Punto',
    'Pantalon',
    'Jeans',
    'Bermuda',
    'Zapatos',
    'Bolsos',
    'Accesorios',
  ];
  ngOnInit() {
    this._brandService
      .getBrand()
      .subscribe((brands) => (this.myBrands = brands));
    this._colorService.getColor().subscribe((colors) => {
      this.myColors = colors;
      if (this.myColors) {
        this.actualSelectedColor = this.myColors[0];
      }
    });
    this.selectedColors = [];
    this.product.colorsIds = [];
  }
  constructor(
    private _brandService: BrandService,
    private _colorService: ColorService,
    private _productService: productService
  ) {
    this.product = new Product();
  }

  createSucceeded(product: Product) {
    this.message =
      'El producto ' + product.name + 'se ha creado correctamente.';
    window.location.reload();
  }

  createFailed(error: ApiMessage) {
    alert(error);
  }
  createColorIdsList() {
    this.selectedColors.forEach((element) =>
      this.product.colorsIds.push(element.id)
    );
  }
  addColor() {
    if (
      this.selectedColors.find(
        (element) => element.id == this.actualSelectedColor.id
      )
    ) {
      this.error = 'This color has already been selected.';
    } else {
      this.selectedColors.push(this.actualSelectedColor);
    }
  }
  removeColor() {
    var pos = this.selectedColors.indexOf(this.actualSelectedColor);
    if (pos == -1) {
      this.error = 'Cant delete the color because it was not selected.';
    } else {
      this.selectedColors.splice(pos);
    }
  }
  getSelectedValue(value: any) {
    this.actualSelectedColor = this.myColors.find(
      (element) => element.id == value
    )!;
  }
  onFormSubmit() {
    if (this.validateFields()) {
      this.createColorIdsList();
      this.product.name = this.name;
      this.product.price = this.price;
      this.product.description = this.description;
      this.product.imageURL = this.url;
      this.product.brandId = this.brand;
      this.product.category = this.productCategory;
      this.product.stock = this.stock;
      this.product.availableForPromotion = this.availableForPromotion;
      this._productService.createProduct(this.product).subscribe(
        (product) => this.createSucceeded(product),
        (error: ApiMessage) => this.createFailed(error)
      );
    }
  }
  close() {
    window.location.reload();
  }
  validateFields() {
    if (
      this.name == undefined ||
      this.description == undefined ||
      this.price == undefined ||
      this.brand == undefined ||
      this.selectedColors == undefined ||
      this.productCategory == undefined ||
      this.stock == undefined ||
      this.availableForPromotion == undefined
    ) {
      this.error = 'All fields must be completed to proceed';
      return false;
    }
    if (this.price <= 0) {
      alert('You must specify a valid price to continue;');
      return false;
    }
    return true;
  }
}
