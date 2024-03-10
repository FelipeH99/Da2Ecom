import { DatePipe } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CartComponent } from './components/cart/cart.component';
import { CreateBrandDiscountComponent } from './components/create-brand-discount/create-brand-discount.component';
import { CreateColorDiscountComponent } from './components/create-color-discount/create-color-discount.component';
import { CreatePercentageDiscountComponent } from './components/create-percentage-discount/create-percentage-discount.component';
import { CreateProductComponent } from './components/create-product/create-product.component';
import { CreateQuantityDiscountComponent } from './components/create-quantity-discount/create-quantity-discount.component';
import { DeleteDiscountComponent } from './components/delete-discount/delete-discount.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { ModifyBrandDiscountComponent } from './components/modify-brand-discount/modify-brand-discount.component';
import { ModifyColorDiscountComponent } from './components/modify-color-discount/modify-color-discount.component';
import { ModifyPercentageDiscountComponent } from './components/modify-percentage-discount/modify-percentage-discount.component';
import { ModifyProductComponent } from './components/modify-product/modify-product.component';
import { ModifyQuantityDiscountComponent } from './components/modify-quantity-discount/modify-quantity-discount.component';
import { PurchaseHistoryComponent } from './components/purchase-history/purchase-history.component';
import { Globals } from './services/globals';
import { LoggedGuard } from './services/guards/logged-guard';
import { LoginGuard } from './services/guards/login-guard';
import { productService } from './services/product.service';
import { AllPurchasesComponent } from './components/all-purchases/all-purchases.component';
import { DeleteUsersComponent } from './components/delete-users/delete-users.component';
import { ModifyUserComponent } from './components/modify-user/modify-user.component';
import { ModifyYourUserComponent } from './components/modify-your-user/modify-your-user.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CreatePercentageDiscountComponent,
    CreateColorDiscountComponent,
    CreateBrandDiscountComponent,
    CreateQuantityDiscountComponent,
    CartComponent,
    HomeComponent,
    CreateProductComponent,
    PurchaseHistoryComponent,
    ModifyProductComponent,
    DeleteDiscountComponent,
    ModifyColorDiscountComponent,
    ModifyBrandDiscountComponent,
    ModifyPercentageDiscountComponent,
    ModifyQuantityDiscountComponent,
    AllPurchasesComponent,
    DeleteUsersComponent,
    ModifyUserComponent,
    ModifyYourUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    Globals,
    LoginGuard,
    LoggedGuard,
    DatePipe,
    productService,
    HttpClient,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
