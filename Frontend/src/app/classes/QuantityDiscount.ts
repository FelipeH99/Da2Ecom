import { ProductCategory } from "./ProductCategory";

export class QuantityDiscount{
  id:string;
  name:string;
  productCategory:ProductCategory;
  category: string;
  minProductNeededForDiscount: number;
  numberOfProductToBeFree: number;
  productToBeDiscounted: string;
  isActive: boolean;

}
