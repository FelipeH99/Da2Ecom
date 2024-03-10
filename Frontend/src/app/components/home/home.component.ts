import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Brand } from 'src/app/classes/Brand';
import { Product } from 'src/app/classes/Product';
import { ProductCategory } from 'src/app/classes/ProductCategory';
import { ProductInformation } from 'src/app/classes/ProductInformation';
import { SearchResult } from 'src/app/classes/SearchResult';
import { BrandService } from 'src/app/services/brand.service';
import { Globals } from 'src/app/services/globals';
import { productService } from 'src/app/services/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass'],
  providers: [productService, BrandService],
})
export class HomeComponent implements OnInit {
  message: string;
  myProducts: Array<SearchResult>;
  myBrands: Brand[];
  minValue: number;
  maxValue: number;
  filterText: string;
  brandId: string;
  category: string;
  availableForPromotion: string;
  errorMessage: string;
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
    private _globals: Globals
  ) {
    this.myProducts = [];
  }

  ngOnInit() {
    this.getProducts().subscribe((prods) => {
      this.myProducts = prods;
    });
    this._brandService.getBrand().subscribe((brands) => {
      this.myBrands = brands;
    });
  }
  getProducts(): Observable<SearchResult[]> {
    return this._productService.getProduct(new ProductInformation());
  }
  addToCart(prodId: string) {
    this._productService.getId(prodId).subscribe((product: Product) => {
      this.appendProduct(product.id);
    });
    this.errorMessage = 'Se ha agregado el item de forma exitosa al carrito!';
  }
  searchProducts() {
    let productInformation = this.createFiltering();
    this._productService
      .getProduct(productInformation)
      .subscribe((products) => {
        this.myProducts = products;
        window.location.reload();
        if (!products) {
          this.message = 'There arent any products to show.';
        }
      });
  }
  appendProduct(productId: string) {
    var previousCartProducts = localStorage.getItem('cart');
    if (previousCartProducts == null) {
      localStorage.setItem('cart', productId);
    } else {
      var newCartProducts = previousCartProducts + '*' + productId;
      localStorage.setItem('cart', newCartProducts);
    }
  }

  createFiltering() {
    let filter = new ProductInformation();
    filter.text = this.filterText;
    filter.brandId = this.brandId;
    filter.category = this.stringToEnum(this.category);
    filter.maxPrice = this.maxValue;
    filter.minPrice = this.minValue;
    filter.availableForPromotion = this.verifyAvailableForPromotion();
    return filter;
  }

  verifyAvailableForPromotion() {
    if (this.availableForPromotion == 'yes') {
      return true;
    }
    return false;
  }
  stringToEnum(name: string): ProductCategory {
    return ProductCategory[name as keyof typeof ProductCategory];
  }
  isLogged() {
    return this._globals.isLogged();
  }
}
