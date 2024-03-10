import { purchaseService } from './../../services/purchase.service';
import { DiscountApplied } from './../../classes/DiscountApplied';
import { Component, OnInit } from '@angular/core';
import { ProductInformation } from 'src/app/classes/ProductInformation';
import { Purchase } from 'src/app/classes/Purchase';
import { SearchResult } from 'src/app/classes/SearchResult';
import { User } from 'src/app/classes/User';
import { BrandService } from 'src/app/services/brand.service';
import { CalculateDiscountService } from 'src/app/services/calculate-discount.service';
import { LoginService } from 'src/app/services/login.service';
import { productService } from 'src/app/services/product.service';
import { Globals } from 'src/app/services/globals';
import { RoleService } from 'src/app/services/role.service';
import { PaymentMethod } from 'src/app/classes/PaymentMethod';
import { paymentMethodService } from 'src/app/services/payment-method.service';
import { ApiMessage } from 'src/app/classes/ApiMessage';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.sass'],
  providers: [
    BrandService,
    LoginService,
    CalculateDiscountService,
    purchaseService,
    RoleService,
    paymentMethodService,
  ],
})
export class CartComponent implements OnInit {
  products: any[];
  quantityOfProduct: number;
  subTotal: number;
  discountApplied: DiscountApplied;
  shipping: number;
  totalPrice: number;
  iva: number;
  message: string;
  payments: Array<PaymentMethod>;
  selectedPaymentMethod: PaymentMethod;
  constructor(
    private _productService: productService,
    private _brandService: BrandService,
    private _login: LoginService,
    private _discount: CalculateDiscountService,
    private _purchaseService: purchaseService,
    private _global: Globals,
    private _paymentMethodService: paymentMethodService
  ) {
    this.products = [];
    this.discountApplied = new DiscountApplied();
    this.shipping = 20;
  }

  ngOnInit() {
    this._paymentMethodService.getPayments().subscribe((payments) => {
      this.payments = payments;
      this.selectedPaymentMethod = payments[0];
    });
    this.getProductsFromStorage();
  }
  getBrand(guid: string) {
    return this._brandService.getId(guid);
  }
  async createPurchase() {
    this.validatePaymentMethod();
    var sessionToken = localStorage.getItem('Token');
    var request = 'POST/PURCHASE';
    var hasPermission = this.hasPermissions(request);
    if (sessionToken && hasPermission) {
      var products = this.products;
      if (products) {
        var purchase = await this.createPurchaseWithUser(sessionToken);
        this._purchaseService.createPurchase(purchase).subscribe(
          (purchase) => (this.createdSuccessful(purchase)),
          (error) => (this.createdFail(error))
        );
      }
    } else if (!hasPermission) {
      alert('You dont have permission to do this action!');
    }
  }
  async createPurchaseWithUser(userSessionToken: string) {
    var purchase = new Purchase();
    var userLogged = (await this._login
      .getUserFromSession(userSessionToken)
      .toPromise()) as User;
    purchase.userId = userLogged.id;
    purchase.productIds = this.createProductList();
    purchase.paymentMethodId = this.selectedPaymentMethod.id;

    return purchase;
  }

  getProductsFromStorage() {
    var productsIds = localStorage.getItem('cart');
    if (productsIds) {
      var prodInfo = new ProductInformation();
      prodInfo.productsIds = productsIds;
      this._productService.getProduct(prodInfo).subscribe((products) => {
        this.products = products;
        this.calculateSubTotal(products);
        this.calculateTotal(products);
      });
    } else {
      this.message = 'No items added to cart!';
    }
  }
  removeProduct(guid: string) {
    var productsIds = localStorage.getItem('cart');
    if (productsIds === guid) {
      localStorage.removeItem('cart');
      location.reload();
    } else if (productsIds?.includes(guid)) {
      var newProductsIds = productsIds.replace(guid + '*', '');
      localStorage.setItem('cart', newProductsIds);
      location.reload();
    }
  }
  productsInCart() {
    if (localStorage.getItem('cart')) {
      return true;
    }
    return false;
  }
  createProductList() {
    var products = localStorage.getItem('cart')?.split('*');
    if (products) {
      return products;
    }
    return [];
  }
  calculateSubTotal(products: SearchResult[]) {
    var localTotal = 0;
    for (var i = 0; i < products.length; i++) {
      localTotal += products[i].price;
    }
    var str = localTotal.toString();
    this.subTotal = Number(str.slice(0, 6));
  }
  calculateTotal(products: SearchResult[]) {
    this._discount.get().subscribe((value) => {
      this.discountApplied = value;
      var iva = this.subTotal * 0.22;
      var str = iva.toString();
      this.iva = Number(str.slice(0, 6));
      this.totalPrice =
        this.subTotal +
        this.iva -
        this.discountApplied.descuento +
        this.shipping;
      var totalPrice = this.totalPrice.toString();
      this.totalPrice = Number(totalPrice.slice(0, 6));
    });
  }
  isLogged() {
    return this._global.isLogged();
  }
  hasPermissions(request: string) {
    if (this.isLogged()) {
      return this._global.hasPermission(request);
    } else {
      return false;
    }
  }
  validatePaymentMethod() {
    if (this.selectedPaymentMethod == undefined) {
      this.message = 'Must select a payment method to proceed.';
      return;
    }
  }
  getSelectedValue(value: any) {
    this.selectedPaymentMethod = this.payments.find(
      (element) => element.id == value
    )!;
    if (this.selectedPaymentMethod == undefined) {
      this.message = 'Must select a payment method to proceed.';
    }
  }
  createdSuccessful(purchase:Purchase){
    if(purchase.id == undefined){
      alert("Make sure you have selected items to buy and you are logged in!");
    }else{
      alert("Compra creada con el identificador " + purchase.id);
      localStorage.removeItem("cart");
    }
    window.location.reload();
  }
  createdFail(error:ApiMessage){
    alert(error);
  }
}
