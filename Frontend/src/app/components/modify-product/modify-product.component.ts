import { Component, OnInit } from '@angular/core';
import { productService } from '../../services/product.service';
import { ProductInformation } from '../../classes/ProductInformation';
import { SearchResult } from '../../classes/SearchResult';
import { ApiMessage } from '../../classes/ApiMessage';
import { Color } from '../../classes/Colors';
import { Brand } from '../../classes/Brand';
import { Product } from '../../classes/Product';
import { BrandService } from '../../services/brand.service';
import { ColorService } from '../../services/color.service';

@Component({
  selector: 'app-modify-product',
  templateUrl: './modify-product.component.html',
  styleUrls: ['./modify-product.component.sass'],
  providers: [BrandService, ColorService],
})
export class ModifyProductComponent implements OnInit {
  products: Array<SearchResult>;
  productToModify: Product;
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
  constructor(
    private _productService: productService,
    private _brandService: BrandService,
    private _colorService: ColorService
  ) {
    this.product = new Product();
    this.products = [];
  }
  ngOnInit() {
    this._productService
      .getProduct(new ProductInformation())
      .subscribe((products) => {
        this.products = products;
      });
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
  showForm(guid: string) {
    this._productService
      .getId(guid)
      .subscribe((product) => (this.productToModify = product));
  }
  delete(guid: string) {
    this._productService.deleteId(guid).subscribe(
      (message) => this.createSucceeded(message),
      (error) => this.createFailed(error)
    );
  }
  createColorIdsList() {
    this.selectedColors.forEach((element) =>
      this.product.colorsIds.push(element.id)
    );
  }

  createFailed(error: string) {
    this.message = error;
  }

  createSucceeded(message: ApiMessage) {
    alert(message);
    window.location.reload();
  }
  addColor() {
    if (
      this.selectedColors.find(
        (element) => element.id == this.actualSelectedColor.id
      )
    ) {
      this.message = 'This color has already been selected.';
    } else {
      this.selectedColors.push(this.actualSelectedColor);
    }
  }
  removeColor() {
    var pos = this.selectedColors.indexOf(this.actualSelectedColor);
    if (pos == -1) {
      this.message = 'Cant delete the color because it was not selected.';
    } else {
      this.selectedColors.splice(pos);
    }
  }
  getSelectedValue(value: any) {
    this.actualSelectedColor = this.myColors.find(
      (element) => element.id == value
    )!;
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
    if (this.price <= 0 || this.stock <= 0) {
      alert('All numeric fields must have values bigger than 0');
      return false;
    }
    return true;
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
      this._productService
        .modifyProduct(this.productToModify.id, this.product)
        .subscribe(
          (product) => this.createSucceeded(product),
          (error) => this.createFailed(error)
        );
    }
  }
}
