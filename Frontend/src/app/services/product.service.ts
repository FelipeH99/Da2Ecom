import { environment } from '../environments/environment';
import { ApiMessage } from '../classes/ApiMessage';
import { Globals } from './globals';
import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { Product } from '../classes/Product';
import { ProductInformation } from '../classes/ProductInformation';
import { SearchResult } from '../classes/SearchResult';

@Injectable()
export class productService {
  constructor(private _httpService: HttpClient) {}

  getProduct(prodInfo: ProductInformation): Observable<Array<SearchResult>> {
    let params = this.createParams(prodInfo);
    const url = `${environment.apiUrl}/products`;
    return this._httpService.get<Array<any>>(url, { params: params }).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }
  getId(id: string): Observable<Product> {
    const url = `${environment.apiUrl}/products/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService.get<Product>(url, httpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }

  createProduct(product: Product): Observable<Product> {
    const url = `${environment.apiUrl}/products`;
    console.log(product);
    const httpOptions = this.getHeaders();
    return this._httpService.post<Product>(url, product, httpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }
  modifyProduct(id: string, product: Product): Observable<ApiMessage> {
    const url = `${environment.apiUrl}/products/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService.put<ApiMessage>(url, product, httpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }
  deleteId(id: string): Observable<ApiMessage> {
    const url = `${environment.apiUrl}/products/${id}`;
    const httpOptions = this.getHeaders();
    return this._httpService.delete<ApiMessage>(url, httpOptions).pipe(
      map((response) => {
        return response;
      }),
      catchError(this.handleError)
    );
  }
  private handleError(error: HttpErrorResponse) {
    let message: string;
    console.log('ERROR: ' + JSON.stringify(error));
    if (error.error instanceof ErrorEvent) {
      message = 'Error: do it again';
    } else {
      if (error.status == 0) {
        message = 'The server is shutdown';
      } else {
        message = error.error;
      }
    }
    return throwError((message: any) => new Error(message));
  }

  private getHeaders() {
    const token = localStorage.getItem('Token');
    const myHeaders = new HttpHeaders({
      Accept: 'application/json',
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      Auth: token ? token : '',
    });
    const httpOptions = { headers: myHeaders };
    return httpOptions;
  }
  private createParams(prodInfo: ProductInformation) {
    let params = new HttpParams();
    params = params.set('text', this.validateText(prodInfo));
    params = params.set('brandId', this.validateBrandId(prodInfo));
    params = params.set('category', this.validateCategory(prodInfo));
    params = params.set('minPrice', this.validateMinPrice(prodInfo));
    params = params.set('maxPrice', this.validateMaxPrice(prodInfo));
    params = params.set(
      'availableForPromotion',
      this.validateAvailability(prodInfo)
    );
    params = params.set('productsIds', this.validateProductsIds(prodInfo));
    return params;
  }
  private validateProductsIds(prodInfo: ProductInformation) {
    if (prodInfo.productsIds != undefined) {
      return prodInfo.productsIds;
    }
    return '';
  }

  private validateText(prodInfo: ProductInformation) {
    if (prodInfo.text != undefined) {
      return prodInfo.text;
    }
    return '';
  }
  private validateBrandId(prodInfo: ProductInformation) {
    if (prodInfo.brandId != undefined) {
      return prodInfo.brandId;
    }
    return '';
  }
  private validateCategory(prodInfo: ProductInformation) {
    if (prodInfo.category != undefined) {
      return prodInfo.category;
    }
    return '';
  }
  private validateMinPrice(prodInfo: ProductInformation) {
    if (prodInfo.minPrice != undefined) {
      return prodInfo.minPrice;
    }
    return '0';
  }
  private validateMaxPrice(prodInfo: ProductInformation) {
    if (prodInfo.maxPrice != undefined) {
      return prodInfo.maxPrice;
    }
    return Number.MAX_VALUE.toString();
  }
  private validateAvailability(prodInfo: ProductInformation) {
    if (prodInfo.availableForPromotion != undefined) {
      return prodInfo.availableForPromotion;
    }
    return 'True';
  }
}
