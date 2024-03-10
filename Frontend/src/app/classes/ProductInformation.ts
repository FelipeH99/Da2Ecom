import { ProductCategory } from "./ProductCategory";

export class ProductInformation{
  text: string;
  brandId: string;
  category: ProductCategory;
  minPrice: number;
  maxPrice: number;
  availableForPromotion: boolean;
  productsIds:string;
}
