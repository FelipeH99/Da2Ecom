import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
import { ModifyQuantityDiscountComponent } from './components/modify-quantity-discount/modify-quantity-discount.component';
import { ModifyProductComponent } from './components/modify-product/modify-product.component';
import { PurchaseHistoryComponent } from './components/purchase-history/purchase-history.component';
import { AllPurchasesComponent } from './components/all-purchases/all-purchases.component';
import { DeleteUsersComponent } from './components/delete-users/delete-users.component';
import { ModifyUserComponent } from './components/modify-user/modify-user.component';
import { ModifyYourUserComponent } from './components/modify-your-user/modify-your-user.component';


const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'cart',
    component: CartComponent
  },
  {
    path: 'createPercentageDiscount',
    component: CreatePercentageDiscountComponent
  },
  {
    path: "createBrandDiscount",
    component: CreateBrandDiscountComponent
  },
  {
    path: "createColorDiscount",
    component: CreateColorDiscountComponent
  },
  {
    path: 'createQuantityDiscount',
    component: CreateQuantityDiscountComponent
  },
  {
    path: "ModifyColorDiscount",
    component: ModifyColorDiscountComponent
  },
  {
    path: "ModifyBrandDiscount",
    component: ModifyBrandDiscountComponent
  },
  {
    path: 'ModifyQuantityDiscount',
    component: ModifyQuantityDiscountComponent
  },
  {
    path: 'ModifyPercentageDiscount',
    component: ModifyPercentageDiscountComponent
  },
  {
    path: 'cart',
    component: CartComponent

  },
  {
    path: 'deleteDiscount',
    component: DeleteDiscountComponent
  },
  {
    path: 'createProduct',
    component: CreateProductComponent
  },
  {
    path: 'modifyProduct',
    component: ModifyProductComponent
  },
  {
    path: 'purchaseHistory',
    component: PurchaseHistoryComponent
  },
  {
    path: 'allPurchases',
    component: AllPurchasesComponent
  },
  {
    path: 'deleteUsers',
    component: DeleteUsersComponent
  },
  {
    path: 'modifyUsers',
    component: ModifyUserComponent
  },
  {
    path: 'modifyYourData',
    component: ModifyYourUserComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
